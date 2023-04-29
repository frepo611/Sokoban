using System;

namespace Sokoban
{


    public class Program
    {                                                                   
        public static void Main(string[] args)
        {

        Level level = new();
        GameManager game = new(level);

            while (true)
            {
                game.DrawLevel();
                if (game.IsComplete()) {
                    Console.SetCursorPosition(0, level.Height + 3);
                    Console.WriteLine($"Level complete!".PadRight(Console.BufferWidth));
                    break;
                }
                var key = Console.ReadKey(true);

                int deltaX = 0;
                int deltaY = 0;

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        deltaX = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        deltaX = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        deltaY = 1;
                        break;
                    case ConsoleKey.UpArrow:
                        deltaY = -1;
                        break;
                    case ConsoleKey.Escape: // exit
                        return;
                    case ConsoleKey.Q: // restart level
                        break;
                }
                game.Move(deltaY, deltaX);
            }
        }
    }
}