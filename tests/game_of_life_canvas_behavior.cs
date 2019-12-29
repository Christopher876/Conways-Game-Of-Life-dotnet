using NUnit.Framework;
using game_of_life;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace game_of_life.tests
{
    public class game_of_life_canvas_behavior
    {
        bool isRunning = false;

        [Test]
        public void game_of_life_Async_Method(){
            Tester();

            while(!isRunning){
                Console.WriteLine(".");
                Thread.Sleep(100);
            }

            Assert.Pass();
        }

        private async void Tester(){
            Console.WriteLine("Starting");
            await Testing();
            Console.WriteLine("Ending");
            isRunning = true;
        }   

        private Task Testing(){
            return Task.Factory.StartNew(()=>{
                Console.WriteLine("Working!");
                Thread.Sleep(5000);
            });
        }   
    }
}