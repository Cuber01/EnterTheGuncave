using System;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.DungeonGenerator
{
    
    public static class DungeonGenerator
    {
        public static RoomPlan[,] floorMap = new RoomPlan[maxFloorWidth, maxFloorHeight];
        
        private static int roomCount;
        private const int minRooms = 20;
        private const int maxRooms = 50;

        private const int maxFloorWidth = 50;
        private const int maxFloorHeight = 50;
        private const int maxNeighborCount = 1; // Neighbor count above which we don't generate a room in an empty cell
        private const float doorChance = 0.5f;

        public static void generate()
        {
            Point startingPos = new Point(maxFloorWidth / 2, maxFloorHeight / 2);
            bool done = false;
            
            floorMap[startingPos.X, startingPos.Y] = new RoomPlan(new RoomInfo(
                1,
                dRoomType.start
                ),
                startingPos);

            // Do your job until it is done. Is it really that hard to understand?
            while (!done)
            {
                floorMap[startingPos.X, startingPos.Y].expand();
                if (roomCount >= minRooms)
                {
                    done = true;
                }
            }
            
            
            debugPrintDungeon();

        }

        public class RoomPlan
        {
            private readonly Point mapPosition;
            public RoomInfo roomInfo;

            private RoomPlan up;
            private RoomPlan down;
            private RoomPlan right;
            private RoomPlan left;

            public RoomPlan(RoomInfo roomInfo, Point mapPosition)
            {
                this.roomInfo = roomInfo;
                this.mapPosition = mapPosition;
            }

            public void expand()
            {
                expandInAllDirs();
                up?.expand();
                down?.expand();
                right?.expand();
                left?.expand();
            }

            private void expandInAllDirs()
            {
                expandAtPoint(new Point(mapPosition.X, mapPosition.Y - 1), dDirection.up);
                expandAtPoint(new Point(mapPosition.X, mapPosition.Y + 1), dDirection.down);
                expandAtPoint(new Point(mapPosition.X + 1, mapPosition.Y), dDirection.right);
                expandAtPoint(new Point(mapPosition.X - 1, mapPosition.Y), dDirection.left);
            }

            private void expandAtPoint(Point newPos, dDirection direction)
            {
                if (floorMap[newPos.X, newPos.Y] != null)
                {
                    return;
                }

                if (roomCount >= maxRooms)
                {
                    return;
                }
                
                if (countNeighbors(newPos) > maxNeighborCount)
                {
                    return;
                }

                if (Util.randomBool(doorChance))
                {
                    return;
                }

                roomCount++;
                floorMap[newPos.X, newPos.Y] = new RoomPlan(new RoomInfo(1, dRoomType.normal), newPos);

                switch (direction)
                {
                    case dDirection.up:
                        up = floorMap[newPos.X, newPos.Y];  break;
                    case dDirection.down:
                        down = floorMap[newPos.X, newPos.Y]; break;
                    case dDirection.right:
                        right = floorMap[newPos.X, newPos.Y]; break;
                    case dDirection.left:
                        left = floorMap[newPos.X, newPos.Y]; break;
                }

            }

            private int countNeighbors(Point cell)
            {
                
                int neighbors = 0;

                if (floorMap[cell.X + 1, cell.Y] != null) neighbors++;
                if (floorMap[cell.X - 1, cell.Y] != null) neighbors++;
                if (floorMap[cell.X, cell.Y + 1] != null) neighbors++;
                if (floorMap[cell.X, cell.Y - 1] != null) neighbors++;

                return neighbors;

            }
            
        }

        private static void debugPrintDungeon()
        {
            string str = "";
            
            
            for (int i = 0; i < floorMap.GetLength(0); i++)
            {
                for (int j = 0; j < floorMap.GetLength(1); j++)
                {
                    if (floorMap[i, j] != null)
                    {
                        str += floorMap[i, j].roomInfo.roomIndex;
                    }
                    else
                    {
                        str += '0';
                    }
                }
            }

            Util.stringToGrid(str);
        }
    }


}