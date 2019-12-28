using NUnit.Framework;
using game_of_life;
using System;

namespace game_of_life.tests
{
    public class game_of_life_canvas_behavior
    {
        [Test]
        public void SetCanvasSize([Values(10)] int row, [Values(10)] int column){
            Canvas canvas = new Canvas();
            var size = canvas.CreateCanvas(length:10,width:10,gridLengthX:200,gridLengthY:200).VertexCount/4;
            Console.WriteLine("Grid Size: " + size);
            Assert.AreEqual(row*column,size);
        }        
    }
}