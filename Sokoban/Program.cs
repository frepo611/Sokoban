using System;
using System.Drawing;

namespace Sokoban;

public class Program
{                                                                   
    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        var continueGame = true;
        var levelId = 1;
        while (continueGame)
        {
            Level level = new(levelId);
            GameManager game = new(level);
            Console.Clear();
            var continueGameRound = true;
            while (continueGameRound && continueGame)
            {
                game.DrawLevel();
                if (game.IsComplete())
                {
                    Console.SetCursorPosition(0, level.Height + 3);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("LEVEL COMPLETE!");
                    Console.WriteLine("Press any key to continue.");
                    Console.ForegroundColor= ConsoleColor.White;
                    levelId++;
                    if (levelId > 3)
                    {
                        Console.SetCursorPosition(0, level.Height + 3);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("ALL LEVELS COMPLETE!");
                        Console.WriteLine("Press any key to exit.".PadRight(Console.BufferWidth));
                        continueGame = false;
                    }
                    continueGameRound = false;
                }
                var key = Console.ReadKey(true);

                Size delta = new(0, 0);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        delta = new(-1,0);
                        break;
                    case ConsoleKey.RightArrow:
                        delta = new(1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        delta = new(0, 1);
                        break;
                    case ConsoleKey.UpArrow:
                        delta = new(0, -1);
                        break;
                    case ConsoleKey.Escape: // exit
                        continueGame = false;
                        break;
                    case ConsoleKey.Q: // restart level
                        continueGameRound = false;
                        break;
                }
                if (delta != new Size(0,0)) 
                {
                    game.Move(delta);
                }
            }
        }
    }
}