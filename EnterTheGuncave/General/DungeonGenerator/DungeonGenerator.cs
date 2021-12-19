using System;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.DungeonGenerator
{
    
    public static class DungeonGenerator
    {
        public static RoomPlan[,] floorMap = new RoomPlan[maxFloorWidth, maxFloorHeight];
        
        private static int roomCount;
        private static int maxRooms;
        
        private const int maxFloorWidth = 25;
        private const int maxFloorHeight = 25;
        private static float doorChance = 0.5f;

        public static void generate()
        {
            Point currentPos = new Point(maxFloorWidth / 2, maxFloorHeight / 2);

            floorMap[currentPos.X, currentPos.Y] = new RoomPlan(new RoomInfo(
                1,
                dRoomType.start
                ),
                currentPos);
            
        }
        
        public class RoomPlan
        {
            private readonly Point mapPosition;
            private RoomInfo roomInfo;

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
                expandAtPoint(new Point(mapPosition.X, mapPosition.Y + 1), dDirection.up);
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

                // TODO make variable
                if (countNeighbors(newPos) > 1)
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
    }


}