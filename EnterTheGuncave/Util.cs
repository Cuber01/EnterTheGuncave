using System;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave
{
    public class Util
    {
        public static void prettyPrint2DArray(int[,] array)
        {
                       
            int rowLength = array.GetLength(0);
            int colLength = array.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.Write(Environment.NewLine );
            }

        }
        
        
        public static int[,] fillInProximityMap(Point target, int[,] map)
        {
            // Where the target is
            map[target.X, target.Y] = 0;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for(int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = Math.Abs(x - target.X) + Math.Abs(y - target.Y);
                }
            }

            return map;
        }

        public static Point getTilePosition(Vector2 pixelPosition, int myWidth, int myHeight)
        {
            Point tilePosition;

            tilePosition.X = (int)((pixelPosition.X + myWidth/2 )  / EnterTheGuncave.tileSize);
            tilePosition.Y = (int)((pixelPosition.Y + myHeight/2) / EnterTheGuncave.tileSize);
            
            return tilePosition;
        }


        
    }
}