using System;

namespace game_of_life
{
    class Program
    {
        static void Main(string[] args)
        {
            Screen screen = new Screen(1920,1080,"Game of Life");
            screen.Game();
        }
    }
}
