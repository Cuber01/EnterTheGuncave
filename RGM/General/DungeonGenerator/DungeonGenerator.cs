using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RGM.General.ContentHandling.Rooms;

// TODO room positions seem flipped towards what the debug print shows

namespace RGM.General.DungeonGenerator
{
    
    public static class DungeonGenerator
    {
        public  static readonly RoomPlan[,] floorMap = new RoomPlan[maxFloorWidth, maxFloorHeight];
        private static readonly List<RoomPlan> endRooms = new List<RoomPlan>();
        public  static Point startingPos;

        // Remember to keep limits which won't go out of bounds of floorMap
        private static int roomCount;
        private static int minRooms = 20;
        private static int maxRooms = 50;

        public const int maxFloorWidth = 50;
        public const int maxFloorHeight = 50;
        private static int maxNeighborCount = 1;
        private static float doorChance = 0.5f;

        public static void generate()
        {
            startingPos = new Point(maxFloorWidth / 2, maxFloorHeight / 2);
            bool done = false;
            
            floorMap[startingPos.X, startingPos.Y] = new RoomPlan(new RoomInfo(
                    Util.random.Next(0, RoomLoader.startRoomCount),
                dRoomType.start
                ),
                startingPos);

            // Do your job until it is done. Is it really that hard to understand?
            while (!done)
            {
                floorMap[startingPos.X, startingPos.Y].expand();
                
                if (roomCount >= minRooms)
                {
                    // Randomly choose special rooms from end rooms.
                    int tresureRoomIndex = Util.random.Next(0, endRooms.Count);
                    endRooms[tresureRoomIndex].roomInfo.roomType = dRoomType.treasure;
                    
                    // Make sure to remove it so we don't place 2 special rooms in one place.
                    endRooms.RemoveAt(tresureRoomIndex);
                    
                    int bossRoomIndex = Util.random.Next(0, endRooms.Count);
                    endRooms[bossRoomIndex].roomInfo.roomType = dRoomType.boss;
                    
                    endRooms.RemoveAt(bossRoomIndex);

                    // We're finished!
                    done = true;
                }
            }
            
            fillInRooms();
            
            //debugPrintDungeon();

        }

        private static void fillInRooms()
        {
            foreach (RoomPlan room in floorMap)
            {
                if (room == null) continue;

                // Fill in rooms with random indexes for each type.
                // Start rooms are assigned from the start.
                switch (room.roomInfo.roomType)
                {
                    
                    case dRoomType.normal:
                    {
                        room.roomInfo.roomIndex = Util.random.Next(0, RoomLoader.normalRoomCount);
                        break;
                    }

                    case dRoomType.treasure:
                    {
                        room.roomInfo.roomIndex = Util.random.Next(0, RoomLoader.treasureRoomCount);
                        break;
                    }

                    case dRoomType.boss:
                    {
                        room.roomInfo.roomIndex = Util.random.Next(0, RoomLoader.bossRoomCount);
                        break;
                    }
                    
                }
            }
        }
        
        public static void setupGeneration(int minRoom=10, int maxRoom=20, int maxNeighbourCount=1, float dooorChance=0.5f)
        {
            Array.Clear(floorMap, 0, floorMap.Length);
            
            minRooms = minRoom;
            maxRooms = maxRoom;
            maxNeighborCount = maxNeighbourCount;
            doorChance = dooorChance;
        }

        public static void clearMap()
        {
            Array.Clear(floorMap, 0, floorMap.Length);
        }

        public class RoomPlan
        {
            private readonly Point mapPosition;
            private bool expanded = false;
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
                
                // If you haven't expanded at all, mark myself as end room and don't try to expand children as they don't exist.
                if (!expanded)
                {
                    endRooms.Add(this);
                    return;
                }
                
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
                floorMap[newPos.X, newPos.Y] = new RoomPlan(new RoomInfo(0, dRoomType.normal), newPos);
                //Util.random.Next(0, RoomLoader.normalRoomCount)
                
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

                expanded = true;

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
                        str += (int)floorMap[i, j].roomInfo.roomType;
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