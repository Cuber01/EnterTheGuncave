using System;
using Microsoft.Xna.Framework;

namespace RGM
{
    public static class Util
    {
        public static Random random = new Random();
        
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
            
            for (int x = 0; x < RGM.currentRoom.GetLength(0); x++)
            {
                for(int y = 0; y < RGM.currentRoom.GetLength(1); y++)
                {
                    
                    if (RGM.currentRoom[x, y] != 0)
                    {
                        map[x, y] = Int32.MaxValue;
                    }
                    
                }
            }
            
            return map;
        }

        public static Point pixelPositionToTilePosition(Vector2 pixelPosition, int myWidth, int myHeight)
        {
            Point tilePosition;

            tilePosition.X = (int)((pixelPosition.X + myWidth / 2 )  / RGM.tileSize);
            tilePosition.Y = (int)((pixelPosition.Y + myHeight/ 2 ) / RGM.tileSize);
            
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

            pixelPosition.X = tilePosition.X * RGM.tileSize ;
            pixelPosition.Y = tilePosition.Y * RGM.tileSize ;
            
            return pixelPosition;
        }

        public static string readFile(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        public static bool randomBool(float chance)
        {
            return random.NextDouble() > chance;
        }

        public static int randomPositiveOrNegative(int number, float chance)
        {
            bool positive = randomBool(chance);

            if (positive)
            {
                return Math.Abs(number);
            }
            else
            {
                return -Math.Abs(number);
            }
        }
        
        public static float straightLineEquation(float x, float a, float b)
        {
            float y = a * x + b;
            return y;
        }

        public static void stringToGrid(string str)
        {
            int l = str.Length;
            int k = 0, row, column;
            row = (int) Math.Floor(Math.Sqrt(l));
            column = (int) Math.Ceiling(Math.Sqrt(l));
 
            if (row * column < l)
            {
                row = column;
            }
 
            char [,]s = new char[row,column];
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if(k < str.Length)
                        s[i,j] = str[k];
                    k++;
                }
            }
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (s[i, j] == 0)
                    {
                        break;
                    }

                    switch (s[i, j])
                    {
                        case '1': Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                        case '2': Console.ForegroundColor = ConsoleColor.Green;   break;
                        case '3': Console.ForegroundColor = ConsoleColor.Yellow;      break;
                        case '4': Console.ForegroundColor = ConsoleColor.Red; break;
                        default:
                            Console.ResetColor();  break;
                    }
                    
                    Console.Write(s[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine("");
            }
        }
        

    }
}