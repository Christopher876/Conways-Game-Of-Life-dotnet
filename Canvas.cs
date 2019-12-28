using System;
using SFML.Graphics;
using SFML.System;

namespace game_of_life
{
    public class Canvas
    {
        public Cell[,] canvas;
        public VertexArray grid;

        private uint length;
        private uint width;

        ///<param name="length">Define the length of the cell</param>
        ///<param name="width">Define the width of the cell</param>
        public Canvas(uint length, uint width){
            this.length = length;
            this.width = width;
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
            return vertexArray;
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

        public void SetupInternalCanvas()
        {
            for (int i = 0; i < canvas.GetLength(0); i++)
            {
                for (int j = 0; j < canvas.GetLength(1); j++)
                {
                    canvas[i,j] = new Cell();
                }
            }
        }
    }
}