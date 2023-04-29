using System.Drawing;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
            Console.SetCursorPosition(_player.Position.X, _player.Position.Y);
            Console.Write('@');

            Console.SetCursorPosition(0, _level.Height + 2);
            Console.WriteLine($"Height: {_level.Height}, Width: {_level.Width}".PadRight(Console.BufferWidth));

        }
    public void Move(int deltaY, int deltaX)
        {
        int newY = _player.Y + deltaY;
        int newX = _player.X + deltaX;

        // hits a wall or a stone in target
        if ((_level.Landscape[newY, newX] == 1) || _level.Stones[newY, newX] == 2)
        {
            return;
        }
        // hits a stone, tries to move it
        if (_level.Stones[newY, newX] == 1)
        {
            int stoneNewY = newY + deltaY;
            int stoneNewX = newX + deltaX;
            // can it be moved here?
            if ((_level.Landscape[stoneNewY, stoneNewX] == 1) ||
                (_level.Stones[stoneNewY, stoneNewX] == 2) ||
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

        _player.Y = newY;
        _player.X = newX;
        _moves++;
        Console.SetCursorPosition(_level.Width + 2, 0);
        Console.WriteLine($"Moves: {_moves}".PadRight(Console.BufferWidth));
        Console.SetCursorPosition(0, _level.Height + 3);
        Console.WriteLine($"PlayerX: {_player.X}, PlayerY: {_player.Y}".PadRight(Console.BufferWidth));
    }

        public bool IsComplete()
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
