using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System;
using System.Numerics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace game_of_life
{
    public class Screen
    {
        private readonly string title;
        private readonly RenderWindow window;
        private readonly View gameView;
        private readonly Color backgroundColor;
        private const uint PanSpeed = 50;
        private readonly Canvas canvas;
        
        public Screen(uint width,uint height,string title, Canvas canvas)
        {
            this.window = new RenderWindow(new VideoMode {Width = width, Height = height},this.title,Styles.Default);
            this.window.SetFramerateLimit(60);
            this.title = title;

            this.gameView = window.GetView();
            this.gameView.Center = (Vector2f)window.Size;
            this.window.SetView(this.gameView);
            
            this.backgroundColor = Color.White;
            
            //Setup key events
            this.window.KeyPressed += Window_KeyPressed;
            //Setup Window Events
            this.window.Closed += (sender,e) => {
                (this.window).Close();
                Environment.Exit(Environment.ExitCode);
            };
            this.window.Resized += (sender,e) => {
                this.gameView.Size = (Vector2f)window.Size;
                this.gameView.Center = new Vector2f(2510,1340);
                this.window.SetView(this.gameView);
            };
            this.canvas = canvas;
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch(e.Code){
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                case Keyboard.Key.Down:
                    gameView.Center = new Vector2f(gameView.Center.X,gameView.Center.Y+PanSpeed);
                    window.SetView(gameView);
                    break;
                case Keyboard.Key.Up:
                    gameView.Center = new Vector2f(gameView.Center.X,gameView.Center.Y-PanSpeed);
                    window.SetView(gameView);
                    break;
                case Keyboard.Key.Left:
                    gameView.Center = new Vector2f(gameView.Center.X-PanSpeed,gameView.Center.Y);
                    window.SetView(gameView);
                    break;
                case Keyboard.Key.Right:
                    gameView.Center = new Vector2f(gameView.Center.X+PanSpeed,gameView.Center.Y);
                    window.SetView(gameView);
                    break;
                case Keyboard.Key.Z:
                    gameView.Zoom(1.1f);
                    window.SetView(gameView);
                    break;
                case Keyboard.Key.X:
                    gameView.Zoom(0.9f);
                    window.SetView(gameView);
                    Math.Clamp(gameView.Size.X,0,1584);
                    Math.Clamp(gameView.Size.Y,0,1188);
                    break;
                case Keyboard.Key.G:
                    canvas.KillAllCells();
                    canvas.GenerateStarters(1000,5000);
                    break;
                case Keyboard.Key.K:
                    canvas.KillAllCells();
                    break;
            }
        }

        public void Game(){
            gameView.Center = new Vector2f(0,0);
            window.SetView(gameView);

            Clock clock = new Clock();
            while(window.IsOpen){
                //Calculate framerate
                float framerate = 1f / clock.ElapsedTime.AsSeconds();
                clock.Restart();
                
                canvas.DrawCells(2);
                window.Clear(backgroundColor);
                window.DispatchEvents();
                
                window.Draw(canvas.Grid);
                window.Draw(canvas.AliveCells);

                window.Display();
                Thread.Sleep(10); //Sleep here so that the game does not run too fast
            }
        }   
    }
}