using System;

namespace Sokoban;


public class GameManager
    {
    private Level _level;
    private (int, int) _playerPos;
    private int _PlayerX;
    private int _PlayerY;

        public GameManager(Level level)
        {
        _level = level;
        _playerPos = GetPlayerPos();
        _PlayerX = _playerPos.Item2;
        _PlayerY = _playerPos.Item1;

        }
        private (int, int) GetPlayerPos()
        {
            for (int y = 0; y < _level.Height; y++)
            {
                for (int x = 0; x < _level.Width; x++)
                {
                    if (_level.Landscape[y, x] == 4)
                    {
                        _level.Landscape[y, x] = 0; // removes the player char
                        return (y, x);
                    }
                }
            }
            throw new Exception();
        }
        
        private static char ReplaceLandcapeGraphic(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '#', // wall
                2 => '$', // target
                _ => ' ',
            };
        }
        private static char ReplaceStonesGraphic(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '.', // stone
                2 => '*', // stone on target
                _ => ' ',
            };
        }
        public void DrawLevel()
        {
            for (int y = 0; y < _level.Height; y++)
            {
                for (int x = 0; x < _level.Width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(ReplaceLandcapeGraphic(_level.Landscape[y, x]));
                }
            }
            for (int y = 0; y < _level.Height; y++)
            {
                for (int x = 0; x < _level.Width; x++)
                {
                    if (_level.Stones[y, x] != 0)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(ReplaceStonesGraphic(_level.Stones[y, x]));
                    }
                }
            }
            Console.SetCursorPosition(_PlayerX, _PlayerY);
            Console.Write('@');

            Console.SetCursorPosition(0, _level.Height + 2);
            Console.WriteLine($"Height: {_level.Height}, Width: {_level.Width}".PadRight(Console.BufferWidth));

        }
        public void Move(int deltaY, int deltaX)
        {

            int newY = _PlayerY + deltaY;
            int newX = _PlayerX + deltaX;

            // hits a wall or a stone in target
            if ((_level.Landscape[newY, newX] == 1) | _level.Stones[newY, newX] == 2)
            {
                return;
            }
            // hits a stone, tries to move it
            if (_level.Stones[newY, newX] == 1)
            {
                int stoneNewY = newY + deltaY;
                int stoneNewX = newX + deltaX;
                // can it be moved here?
                if ((_level.Landscape[stoneNewY, stoneNewX] == 1) |
                    (_level.Stones[stoneNewY, stoneNewX] == 2) |
                    (_level.Stones[stoneNewY, stoneNewX] == 1))
                {
                    return;
                }

                // updating the stones
                _level.Stones[newY, newX] = 0;
                _level.Stones[stoneNewY, stoneNewX] = 1;

                // updating the landscape, hit a target?
                if (_level.Landscape[stoneNewY, stoneNewX] == 2)
                {
                    _level.Stones[stoneNewY, stoneNewX] = 2;
                    _level.Landscape[stoneNewY, stoneNewX] = 0;
                }
            }
            _PlayerY = newY;
            _PlayerX = newX;
            Console.SetCursorPosition(0, _level.Height + 3);
            Console.WriteLine($"PlayerX: {_PlayerX}, PlayerY: {_PlayerY}".PadRight(Console.BufferWidth));
        }

        internal bool IsComplete()
        {
            for (int y = 0; y < _level.Height; y++)
            {
                for (int x = 0; x < _level.Width; x++)
                {
                    if (_level.Landscape[y, x] == 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
