using System;

namespace Sokoban;

public class Program
{                                                                   
    public static void Main(string[] args)
    {
        var continueGame = true;
        while (continueGame)
        {
            Level level = new();
            GameManager game = new(level);

            var continueGameRound = true;
            while (continueGameRound && continueGame)
            {
                game.DrawLevel();
                if (game.IsComplete())
                {
                    Console.SetCursorPosition(0, level.Height + 3);
                    Console.WriteLine($"Level complete! Press any key to continue.".PadRight(Console.BufferWidth));
                    continueGameRound = false;
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
                        continueGame = false;
                        break;
                    case ConsoleKey.Q: // restart level
                        continueGameRound = false;
                        break;
                }
                game.Move(deltaY, deltaX);
            }
        }
    }
}