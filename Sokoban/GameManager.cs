﻿using System.Drawing;

namespace Sokoban;


public class GameManager
{
    private Level _level;
    private Player _player;
    private int _moves;


    class Player
    {
        public Point Position { get; set; }

        public Player(Point startPosition)
        {
            Position = startPosition;
        }

    }
    public GameManager(Level level)
        {
        _moves = 0;
        _level = level;
        _player = new Player(GetPlayerPos());
        

        }
    private Point GetPlayerPos()
        {
            for (int y = 0; y < _level.Height; y++)
            {
                for (int x = 0; x < _level.Width; x++)
                {
                    if (_level.Landscape[y, x] == 4)
                    {
                        _level.Landscape[y, x] = 0; // removes the player char
                        return new Point(x,y);
                    }
                }
            }
            throw new Exception("Player starting position not found!");
        }
        
    private static char ReplaceLandcapeGraphic(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '#', // wall
                2 => '$', // target
                _ => throw new Exception("Unknown character in Landscape.")
            };
        }
    private static char ReplaceStonesGraphic(int input)
        {
            return input switch
            {
                0 => ' ',
                1 => '.', // stone
                2 => '*', // stone on target
                _ => throw new Exception("Unknown character in Stones.")
            };
        }
    public void DrawLevel()
    {
        Console.SetCursorPosition(_level.Width + 2, 1);
        Console.Write($"Level Id: {_level.LevelId}".PadRight(Console.BufferWidth));

        Console.SetCursorPosition(_level.Width + 2, 0);
        Console.Write($"Stone moves: {_moves}".PadRight(Console.BufferWidth));

        Console.SetCursorPosition(_level.Width + 2, 3);
        Console.WriteLine("Use arrow keys to move.".PadRight(Console.BufferWidth));

        Console.SetCursorPosition(_level.Width + 2, 4);
        Console.WriteLine("Press 'Q' to restart the level".PadRight(Console.BufferWidth));

        Console.SetCursorPosition(_level.Width + 2, 5);
        Console.WriteLine("Press 'Esc' to quit".PadRight(Console.BufferWidth));

        // draw the level, except stones and stones on target
        for (int y = 0; y < _level.Height; y++)
        {
            for (int x = 0; x < _level.Width; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(ReplaceLandcapeGraphic(_level.Landscape[y, x]));
            }
        }
        // draw stones and stones on target
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
        //draw player
        Console.SetCursorPosition(_player.Position.X, _player.Position.Y);
        Console.Write('@');
    }
    public void Move(Size delta)
    {
        Point newPosition = Point.Add(_player.Position, delta);
        
        // hits a wall or a stone in target
        if ((_level.Landscape[newPosition.Y, newPosition.X] == 1) || _level.Stones[newPosition.Y, newPosition.X] == 2)
        {
            return;
        }
        // hits a stone, tries to move it
        if (_level.Stones[newPosition.Y, newPosition.X] == 1)
        {
            Point newStonePosition = Point.Add(newPosition, delta);

            // can it be moved here?
            if ((_level.Landscape[newStonePosition.Y, newStonePosition.X] == 1) ||
                (_level.Stones[newStonePosition.Y, newStonePosition.X] == 2) ||
                (_level.Stones[newStonePosition.Y, newStonePosition.X] == 1))
            {
                return;
            }

            // updating the stones
            _level.Stones[newPosition.Y, newPosition.X] = 0;
            _level.Stones[newStonePosition.Y, newStonePosition.X] = 1;
            _moves++;

            // updating the landscape, hit a target?
            if (_level.Landscape[newStonePosition.Y, newStonePosition.X] == 2)
            {
                _level.Stones[newStonePosition.Y, newStonePosition.X] = 2;
                _level.Landscape[newStonePosition.Y, newStonePosition.X] = 0;
            }
        }
        _player.Position = newPosition;
    }

        public bool IsComplete()
        //check for targets left
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
