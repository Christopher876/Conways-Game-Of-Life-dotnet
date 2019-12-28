using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System;
using System.Numerics;

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

        private List<RectangleShape> DrawGrid(int size = 50, int gridNumber = 50){
            List<RectangleShape> rectangles = new List<RectangleShape>();

            float rectSizeX = window.Size.X / 20;
            float rectSizeY = window.Size.Y / 20;

            bool alt_color = false;

            for(int y = 0; y < size;y++){
                for(int x = 0; x < size; x++){
                    rectangles.Add(new RectangleShape(new Vector2f(rectSizeX,rectSizeY)){
                        Position = new Vector2f(rectSizeX*x,rectSizeY*y),
                        OutlineColor = Color.Black,
                        FillColor = alt_color ? Color.Red:Color.Blue,
                        OutlineThickness = 1.5f,
                    });
                    alt_color = !alt_color;
                }
            }
            return rectangles;
        }

        public void Game(){
            view.Center = new Vector2f(0,0);
            window.SetView(view);

            VertexArray triangle = new VertexArray(PrimitiveType.Triangles,3);
            triangle.Append(new Vertex(){Position = new Vector2f(200,200),Color=Color.Red});
            triangle.Append(new Vertex(){Position = new Vector2f(300,200),Color=Color.Red});
            triangle.Append(new Vertex(){Position = new Vector2f(300,300),Color=Color.Red});


            var l = new Canvas();
            var rectangle = new VertexArray(PrimitiveType.Quads);

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    var v = l.DrawCells((uint)new Random().Next(0,400),(uint)new Random().Next(0,400),23,27,2);
                    foreach (var item in v)
                    {
                        rectangle.Append(item);
                    }
                }
            }

            l.CreateCanvas(gridLengthX:32*594,gridLengthY:32*594);

            Clock clock = new Clock();

            while(window.IsOpen){
                //Calculate framerate
                float Framerate = 1f / clock.ElapsedTime.AsSeconds();
                clock.Restart();

                window.Clear(backgroundColor);
                window.DispatchEvents();

                //Draw fps
                fps.DisplayedString = String.Format("{0:0.##}",Framerate);
                fps.Position = new Vector2f(view.Center.X-380,view.Center.Y-300);                
                window.Draw(fps);
                window.Draw(l.grid);
                window.Draw(rectangle);

                window.Display();
            }
        }   
    }
}