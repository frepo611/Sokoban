using System;
namespace Sokoban;

public class Level
{
    public int[,] Landscape { get; set; }
    public int[,] Stones { get; set; }
    public int Height
    {
        get
        {
            return Landscape.GetLength(0);
        }
    
    }
    public int Width 
    {
        get
        {
            return Landscape.GetLength(1);
        }
    }

    public Level()
	{
        Landscape = new[,] {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,4,1,0,0,0,0,0,0,1,1},
            {1,0,0,1,2,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,2,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1},
         };
        Stones = new[,]  {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
        };
    }


    
}

