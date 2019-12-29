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
        private string title;
        private VideoMode videoMode;
        private RenderWindow window;
        private View view;
        private readonly uint fontSize;
        private Font sourceCodeFont;
        private Text fps;
        private Color backgroundColor;
        private uint panSpeed = 10;
        public Screen(uint width,uint height,string title){
            this.videoMode = new VideoMode();
            this.videoMode.Width = width;
            this.videoMode.Height = height;
            this.window = new RenderWindow(videoMode,this.title,Styles.Default);
            //this.window.SetFramerateLimit(60);
            this.title = title;
            this.fontSize = (uint)(window.Size.X/9/2);
            this.sourceCodeFont = new Font(@"SourceCodeVariable-Roman.ttf");

            this.view = window.GetView();
            this.view.Center = new Vector2f(1965,1020);
            this.window.SetView(this.view);
            
            this.fps = new Text(){
                FillColor = Color.Blue,
                Font = sourceCodeFont,
                Position = new Vector2f(view.Center.X-380,view.Center.Y-300),
                Style = Text.Styles.Bold
            };
            this.backgroundColor = Color.White;

            //Setup key events
            this.window.KeyPressed += Window_KeyPressed;
            //Setup Window Events
            this.window.Closed += (sender,e) => {
                (this.window).Close();
                Environment.Exit(Environment.ExitCode);
            };
            this.window.Resized += (sender,e) => {
                this.view.Size = (Vector2f)window.Size * 2;
                window.SetView(this.view);
            };
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch(e.Code){
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                case Keyboard.Key.Down:
                    view.Center = new Vector2f(view.Center.X,view.Center.Y+panSpeed);
                    window.SetView(view);
                    break;
                case Keyboard.Key.Up:
                    view.Center = new Vector2f(view.Center.X,view.Center.Y-panSpeed);
                    window.SetView(view);
                    break;
                case Keyboard.Key.Left:
                    view.Center = new Vector2f(view.Center.X-panSpeed,view.Center.Y);
                    window.SetView(view);
                    break;
                case Keyboard.Key.Right:
                    view.Center = new Vector2f(view.Center.X+panSpeed,view.Center.Y);
                    window.SetView(view);
                    break;
                case Keyboard.Key.Z:
                    view.Zoom(1.1f);
                    fps.Position = new Vector2f(view.Center.X-380,view.Center.Y-300);
                    window.SetView(view);
                    break;
                case Keyboard.Key.X:
                    view.Zoom(0.9f);
                    fps.Position = new Vector2f(view.Size.X-380,view.Size.Y-300);
                    window.SetView(view);
                    Math.Clamp(view.Size.X,0,1584);
                    Math.Clamp(view.Size.Y,0,1188);
                    break;
            }
            Console.WriteLine($"Camera View: {view.Center}");            
            
        }

        public void Game(){
            view.Center = new Vector2f(0,0);
            window.SetView(view);

            var l = new Canvas(23,27);
            var rectangle = new VertexArray(PrimitiveType.Quads);
            l.CreateCanvas(100,100);
            l.SetupInternalCanvas();

            l.canvas[0,1].cellState = CellState.Alive;
            l.canvas[5,8].cellState = CellState.Alive;
            l.canvas[7,7].cellState = CellState.Alive;
            l.canvas[0,99].cellState = CellState.Alive;
            l.canvas[99,99].cellState = CellState.Alive;
            l.canvas[99,0].cellState = CellState.Alive;
            
            Clock clock = new Clock();
            while(window.IsOpen){
                //Calculate framerate
                float Framerate = 1f / clock.ElapsedTime.AsSeconds();
                clock.Restart();

                l.DrawCells(2);

                window.Clear(backgroundColor);
                window.DispatchEvents();

                //Draw fps
                fps.DisplayedString = String.Format("{0:0.##}",Framerate);
                fps.Position = new Vector2f(view.Center.X-380,view.Center.Y-300);                
                window.Draw(fps);
                window.Draw(l.grid);

                window.Draw(l.aliveCells);

                window.Display();
            }
        }   
    }
}