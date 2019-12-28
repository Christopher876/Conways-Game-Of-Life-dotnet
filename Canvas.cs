using SFML.Graphics;
using SFML.System;

namespace game_of_life
{
    public class Canvas
    {
        public readonly int[,] canvas;
        public VertexArray grid;

        ///<param name="row">Define the number of rows</param>
        ///<param name="column">Define the number of columns</param>
        public Canvas(int row=0, int column=0){
            this.canvas = new int[row,column];
        }

        public VertexArray CreateCanvas(uint row = 10, uint column = 10, uint length=23, uint width=27, uint gridLengthX=594, uint gridLengthY=594){
            VertexArray vertexArray = new VertexArray(PrimitiveType.Quads);

            for (int x = 0; x < (gridLengthX/width)+1; x++)
            {
                vertexArray.Append(new Vertex(){Position = new Vector2f(width * x,0),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f((width * x)+2,0),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f((width * x)+2,gridLengthX+4),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f(width * x,gridLengthX+4),Color=Color.Red});   
            }

            for (int y = 0; y < ((gridLengthY+4)/length)+1; y++)
            {
                vertexArray.Append(new Vertex(){Position = new Vector2f(0,y * length),Color=Color.Red}); //the 5s control the starting
                vertexArray.Append(new Vertex(){Position = new Vector2f(gridLengthY+2,y * length),Color=Color.Red});
                vertexArray.Append(new Vertex(){Position = new Vector2f(gridLengthY+2,(y*length)+2),Color=Color.Red}); //change 2 for our thickness and 100 for length
                vertexArray.Append(new Vertex(){Position = new Vector2f(0,(y*length)+2),Color=Color.Red});
            }
            this.grid = vertexArray;
            return vertexArray;
        }
        
        ///<param name="x">
        public Vertex[] DrawCells(uint x, uint y, uint length, uint width, int boxOffset)
        {
            Vertex[] vertexArray = new Vertex[4];
            vertexArray[0] = (new Vertex(){Position = new Vector2f(boxOffset+(width*x),boxOffset+(length*y)),Color=Color.Black});
            vertexArray[1] = (new Vertex(){Position = new Vector2f(width+(width*x),boxOffset+(length*y)),Color=Color.Black});
            vertexArray[2] = (new Vertex(){Position = new Vector2f(width+(width*x),length+(length*y)),Color=Color.Black});
            vertexArray[3] = (new Vertex(){Position = new Vector2f(boxOffset+(width*x),length+(length*y)),Color=Color.Black});
            return vertexArray;
        }
    }
}