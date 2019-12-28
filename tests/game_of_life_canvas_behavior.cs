using NUnit.Framework;
using game_of_life;
using System;

namespace game_of_life.tests
{
    public class game_of_life_canvas_behavior
    {
        [Test]
        public void SetCanvasSize([Values(10)] int row, [Values(10)] int column){
            Canvas canvas = new Canvas(23,27);
            var size = canvas.CreateCanvas(20,20).VertexCount/4;
            Console.WriteLine("Grid Size: " + size);
            Assert.AreEqual(row*column,size);
        }        
    }
}