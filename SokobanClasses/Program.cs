using System;
using XMLSerialisering;

namespace SokobanClasses
{
    class Level
    {
        private int[,] _landscape;
        private int[,] _stones;
        private int _levelHeight;
        private int _levelWidth;
        private (int,int) _playerPos;
        private int _PlayerX;
        private int _PlayerY;
        public Level(int[,] landscape, int [,] stones)
        {
            _landscape = landscape;
            _stones = stones;
            _levelHeight = _landscape.GetLength(0);
            _levelWidth = _landscape.GetLength(1);
            _playerPos = getPlayerPos();
            _PlayerX = _playerPos.Item2;
            _PlayerY = _playerPos.Item1;
            
        }
        private (int,int) getPlayerPos() 
        {
            for (int y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    if (_landscape[y, x] == 4)
                    {    
                        _landscape[y, x] = 0; // removes the player char
                        return (y, x);
                    }
                }
            }throw new Exception();
        }

        public int getLevelHeight()
        {
            return _levelHeight;
        }
        private static char ReplaceLevelChar(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '#',
                2 => '$',
                _ => ' ',
            };
        }
        private static char ReplaceStonesChar(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '.',
                2 => '*',
                _ => ' ',
            };
        }
        public void DrawLevel() 
        {
            for (int y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(ReplaceLevelChar(_landscape[y, x]));
                }
            }
            for (int y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    if (_stones[y, x] != 0)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(ReplaceStonesChar(_stones[y, x]));
                    }
                }
            }
            Console.SetCursorPosition(_PlayerX, _PlayerY);
            Console.Write('@');

            Console.SetCursorPosition(0, _levelHeight + 2);
            Console.WriteLine($"Height: {_levelHeight}, Width: {_levelWidth}".PadRight(Console.BufferWidth));

        }
        public void Move(int deltaY, int deltaX)
        {

            int newY = _PlayerY + deltaY;
            int newX = _PlayerX + deltaX;

            // hits a wall or a stone in target
            if ((_landscape[newY, newX] == 1) || _stones[newY, newX] == 2)
            {
                Console.SetCursorPosition(0, _levelHeight + 4);
                Console.WriteLine($"Hit a {_landscape[newY, newX]}".PadRight(Console.BufferWidth));
                return;
            }
            // hits a stone, tries to move it
            if (_stones[newY, newX] == 1)
            {
                int stoneNewY = newY + deltaY;
                int stoneNewX = newX + deltaX;
                // can it be moved here?
                if ((_landscape[stoneNewY, stoneNewX] == 1) ||
                    (_stones[stoneNewY, stoneNewX] == 2) ||
                    (_stones[stoneNewY, stoneNewX] == 1))
                {
                    return;
                }

                // updating the stones
                _stones[newY, newX] = 0;
                _stones[stoneNewY, stoneNewX] = 1;

                // updating the level, hit a target?
                if (_landscape[stoneNewY, stoneNewX] == 2)
                {
                    _stones[stoneNewY, stoneNewX] = 2;
                    _landscape[stoneNewY, stoneNewX] = 0;
                }
            }
            _PlayerY = newY;
            _PlayerX = newX;
            Console.SetCursorPosition(0, _levelHeight + 3);
            Console.WriteLine($"PlayerX: {_PlayerX}, PlayerY: {_PlayerY}".PadRight(Console.BufferWidth));
        }

        internal bool IsComplete()
        {
            for (int y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    if (_landscape[y, x] == 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    internal class Program
    {
        public static int[,] landscape =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,4,1,0,0,0,0,0,0,1,1},
            {1,0,0,1,2,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,2,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1},
         };

        public static int[,] stones =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
        };


        static void Main(string[] args)
        {


        Level level = new Level(landscape, stones);
            while (true)
            {
                level.DrawLevel();
                if (level.IsComplete()) {
                    Console.SetCursorPosition(0, level.getLevelHeight() + 3);
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
                }
                level.Move(deltaY, deltaX);
            }
        }
    }
}