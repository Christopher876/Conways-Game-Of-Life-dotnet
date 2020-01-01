using System;
using CommandLine;
using CommandLine.Core;

namespace game_of_life
{
    public class Options
    {
        [Option('s', "size", Required = false,
            HelpText = "Set the size of the grid that will be generated. E.G. 20 will be a 20x20 grid", Default = 100)]
        public int GridSize { get; set; }
        
        [Option('l',"min",Required = false,HelpText = "Set the minimum range for the random seed", Default = 20)]
        public int MinRange { get; set; }
        
        [Option('m',"max",Required = false,HelpText = "Set the maximum range for the random seed", Default = 5000)]
        public int MaxRange { get; set; }
        
        [Option('l',"length",Required = false,HelpText = "Set the length of each cell", Default = 23)]
        public int Length { get; set; }
        
        [Option('w',"width",Required = false,HelpText = "Set the width of each cell",Default = 27)]
        public int Width { get; set; }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => { options = o; });
            
            Canvas canvas = new Canvas((uint)options.Length,(uint)options.Width,options.MinRange,options.MaxRange);
            canvas.CreateCanvas((uint)options.GridSize,(uint)options.GridSize);
            Screen screen = new Screen(800,600,"Game of Life",canvas);
            screen.Game();
        }
    }
}
