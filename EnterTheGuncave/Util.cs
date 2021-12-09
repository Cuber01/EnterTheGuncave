using System;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave
{
    public static class Util
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

        public static Point pixelPositionToTilePosition(Vector2 pixelPosition, int myWidth, int myHeight)
        {
            Point tilePosition;

            tilePosition.X = (int)((pixelPosition.X + myWidth / 2 )  / EnterTheGuncave.tileSize);
            tilePosition.Y = (int)((pixelPosition.Y + myHeight/ 2 ) / EnterTheGuncave.tileSize);
            
            return tilePosition;
        }

        public static float calculateDistance(Vector2 point1, Vector2 point2)
        {
            float dx = Math.Abs(point2.X - point1.X);
            float dy = Math.Abs(point2.Y - point1.Y);
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            return dist;
        }
        
        public static Vector2 tilePositionToPixelPosition(Point tilePosition)
        {
            Vector2 pixelPosition;

            pixelPosition.X = tilePosition.X * EnterTheGuncave.tileSize ;
            pixelPosition.Y = tilePosition.Y * EnterTheGuncave.tileSize ;
            
            return pixelPosition;
        }


        
    }
}