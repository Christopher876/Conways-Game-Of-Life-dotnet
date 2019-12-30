using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace game_of_life
{
    public class Canvas
    {
        public Cell[,] canvas;
        public VertexArray grid;
        public VertexArray aliveCells;

        private uint length;
        private uint width;

        ///<param name="length">Define the length of the cell</param>
        ///<param name="width">Define the width of the cell</param>
        public Canvas(uint length, uint width){
            this.length = length;
            this.width = width;
            this.aliveCells = new VertexArray(PrimitiveType.Quads);
        }

        /// <summary>
        /// Create a new grid, only with the same numbers
        /// </summary>
        /// <param name="row">Number of rows to be created</param>
        /// <param name="column">Number of columns to be created</param>
        /// <returns>Returns a VertexArray with the grid</returns>
        public VertexArray CreateCanvas(uint row = 10, uint column = 10){
            VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);
            for (int x = 0; x < row+1; x++)
            {
                vertexArray.Append(new Vertex(){Position = new Vector2f(width * x,0),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f((width * x)+2,0),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f((width * x)+2,length * row),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f(width * x,length*row),Color=Color.Red});   
            }

            for (int y = 0; y < column+1; y++)
            {
                vertexArray.Append(new Vertex(){Position = new Vector2f(0,y * length),Color=Color.Red}); //the 5s control the starting
                vertexArray.Append(new Vertex(){Position = new Vector2f(width*column+2,y * length),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f(width*column+2,(y*length)+2),Color=Color.Red}); //change 2 for our thickness and 100 for length
                vertexArray.Append(new Vertex(){Position = new Vector2f(0,(y*length)+2),Color=Color.Red});
            }

            grid = vertexArray;
            canvas = new Cell[row,column]; //Create our internal grid
            //Initialize the canvas array
            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    canvas[i,j] = new Cell();
                }
            }
            return vertexArray;
        }

        /// <summary>
        /// Generate the starting positions of the alive cells using a range.
        /// </summary>
        /// <param name="minimum">The minimum number of cells to generate</param>
        /// <param name="maximum">The maximum number of cells to generate</param>
        public void GenerateStarters(int minimum, int maximum){
            var r = new Random();
            var j = r.Next(minimum,maximum);
            for (int i = 0; i < j; i++)
            {
                canvas[r.Next(0,canvas.GetLength(0)-1),r.Next(0,canvas.GetLength(1)-1)].cellState = CellState.Alive;
            }
        }

        public void KillAllCells(){
            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    lock(canvas){
                        canvas[i,j].cellState = CellState.Dead;
                    }
                }
            }
        }

        /// <summary>
        /// Fill any cell in the grid with a Color
        /// </summary>
        /// <param name="x">X coordinate of cell that needs to be filled</param>
        /// <param name="y">Y coordinate of cell that needs to be filled</param>
        /// <param name="boxOffset">Offset of where the box should be drawn in case it is not in the ideal x position</param>
        /// <returns>Array of vertices</returns>
        public Vertex[] DrawCells(uint x, uint y, int boxOffset=0)
        {
            Vertex[] vertexArray = new Vertex[4];
            vertexArray[0] = (new Vertex(){Position = new Vector2f(boxOffset+(width*x),boxOffset+(length*y)),Color=Color.Black});
            vertexArray[1] = (new Vertex(){Position = new Vector2f(width+(width*x),boxOffset+(length*y)),Color=Color.Black});
            vertexArray[2] = (new Vertex(){Position = new Vector2f(width+(width*x),length+(length*y)),Color=Color.Black});
            vertexArray[3] = (new Vertex(){Position = new Vector2f(boxOffset+(width*x),length+(length*y)),Color=Color.Black});
            return vertexArray;
        }
        /// <summary>
        /// Check which cells should live to the next generation and which should die.
        /// </summary>
        /// <returns>Tuple containing all cells to draw</returns>
        public List<Tuple<int,int>> CheckCells(){
            List<Tuple<int,int>> aliveCells = new List<Tuple<int,int>>();
            Cell[,] grid2 = new Cell[canvas.GetLength(0)+1,canvas.GetLength(1)+1]; //Copy the grid so that the original won't be modified while we change cell states

            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    grid2[i,j] = canvas[i,j];
                }
            }

            for (int i = 0; i < canvas.GetLength(0); i++) //Moving down rows
            {
                for (int y = 0; y < canvas.GetLength(1); y++) //Moving across the columns
                {
                    int neighbors = 0;
 
                    //Check around the cells for any neighbors
                    for (int a = -1; a < 2; a++)
                    {
                        for (int b = -1; b < 2; b++)
                        {
                            if(!(a == 0 && b == 0)) //Do not count self
                                if(grid2[(i + a + canvas.GetLength(0)) % canvas.GetLength(0),(y + b + canvas.GetLength(1)) % canvas.GetLength(1)].cellState == CellState.Alive)
                                    neighbors++;
                        }
                    }

                    canvas[i,y].Update(neighbors); //Cell check which state it should be in

                    //Add our cell to the array to be drawn
                    if(canvas[i,y].cellState == CellState.Alive){
                        aliveCells.Add(Tuple.Create(i,y));
                    }
                }
            }

            return aliveCells;
        }

        /// <summary>
        /// Check and create the cells to be drawn to the screen.
        /// </summary>
        /// <param name="boxOffset">The offset of the drawing of the box</param>
        public void DrawCells(int boxOffset){
            aliveCells.Clear();
            var t = CheckCells();

            for (int i = 0; i < t.Count; i++)
            {
                aliveCells.Append(new Vertex(){Position = new Vector2f(boxOffset+(width*t[i].Item1),boxOffset+(length*t[i].Item2)),Color=Color.Black});
                aliveCells.Append(new Vertex(){Position = new Vector2f(width+(width*t[i].Item1),boxOffset+(length*t[i].Item2)),Color=Color.Black});
                aliveCells.Append(new Vertex(){Position = new Vector2f(width+(width*t[i].Item1),length+(length*t[i].Item2)),Color=Color.Black});
                aliveCells.Append(new Vertex(){Position = new Vector2f(boxOffset+(width*t[i].Item1),length+(length*t[i].Item2)),Color=Color.Black});
            }
        }
    }
}