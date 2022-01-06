using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using RGM.Entities.Allies;
using RGM.Entities.Baddies;
using RGM.Entities.Neutrals;
using RGM.General.DungeonGenerator;
using RGM.Items;


namespace RGM.General.ContentHandling.Rooms
{
    public static class RoomLoader
    {
        private static readonly List<Room> normalRooms  = new List<Room>();
        private static readonly List<Room> treasureRooms = new List<Room>();
        private static readonly List<Room> bossRooms    = new List<Room>();
        private static readonly List<Room> startRooms    = new List<Room>();
        
        public static int normalRoomCount;
        public static int treasureRoomCount;
        public static int bossRoomCount;
        public static int startRoomCount;
        
        public static bool changingRoom = false;

        public static void loadAllRooms()
        {
            
            string mapPath = String.Format(".{0}Content{0}assets{0}maps{0}", Path.DirectorySeparatorChar);

            string[] normalRoomFiles   = Directory.GetFiles(mapPath + "normal");
            string[] treasureRoomFiles = Directory.GetFiles(mapPath + "treasure");
            string[] bossRoomFiles     = Directory.GetFiles(mapPath + "boss");
            string[] startRoomFiles    = Directory.GetFiles(mapPath + "start");
            
            normalRoomCount   = normalRoomFiles.Length;
            treasureRoomCount = treasureRoomFiles.Length;
            bossRoomCount     = bossRoomFiles.Length;
            startRoomCount    = startRoomFiles.Length;
            
            foreach (string file in normalRoomFiles)
            {
                normalRooms.Add(loadRoom(file));
            }
            
            foreach (string file in treasureRoomFiles)
            {
                treasureRooms.Add(loadRoom(file));
            }
            
            foreach (string file in bossRoomFiles)
            {
                bossRooms.Add(loadRoom(file));
            }
            
            foreach (string file in startRoomFiles)
            {
                startRooms.Add(loadRoom(file));
            }
            
        }
        
        private static Room loadRoom(string path)
        {
            string levelJSON = File.ReadAllText(path);

            return Room.FromJson(levelJSON);
        }

        public static void playRoom(int index, dRoomType roomType, dDirection wherePlayerCameFrom)
        {
            // Clear the current room, place walls in the new one
            clearRoom();
            placeWalls();

            // Place the player according to where he came from (eg. if he came from the left, place him on the right)
            placePlayer(wherePlayerCameFrom);

            List<Room> allRoomsOfType = new List<Room>();

            switch (roomType)
            {
                case dRoomType.normal:   allRoomsOfType  =  normalRooms; break;
                case dRoomType.boss:     allRoomsOfType  =  bossRooms; break;
                case dRoomType.treasure: allRoomsOfType  =  treasureRooms; break;
                case dRoomType.start:    allRoomsOfType  =  startRooms; break;
            }

            for(int i = 0; i < allRoomsOfType[index].Layers[0].Data.Length; i++)
            {
                if (allRoomsOfType[index].Layers[0].Data[i] == 0) continue;
                
                int currentCol = i / RGM.roomWidth;
                int colIndex = i % RGM.roomWidth;

                switch (allRoomsOfType[index].Layers[0].Data[i])
                {
                    case 1:
                        RGM.entitiesToBeSpawned.Add(new Stone(
                            new Vector2(colIndex * RGM.tileSize, currentCol * RGM.tileSize)));
                        break;
                    
                    case 2: RGM.entitiesToBeSpawned.Add(new Pedestal
                            (new Vector2(colIndex * RGM.tileSize, currentCol * RGM.tileSize),
                                Activator.CreateInstance(RGM.allItems[Util.random.Next(0, RGM.allItems.Count)]) as BaseItem
                            ));
                        break;

                    case 5:
                        RGM.enemiesInRoom++;
                        RGM.entitiesToBeSpawned.Add(new TurretEnemy
                            (new Vector2(colIndex * RGM.tileSize, currentCol * RGM.tileSize)));
                        break;
                    
                    case 6:
                        RGM.enemiesInRoom++;
                        RGM.entitiesToBeSpawned.Add(new WalkingEnemy
                            (new Vector2(colIndex * RGM.tileSize, currentCol * RGM.tileSize)));
                        break;
                    
                }

                RGM.currentRoom[colIndex, currentCol] = Int32.MaxValue;
            }

            foreach(var nonTileObj in allRoomsOfType[index].Layers[1].Objects)
            {
                
                switch (nonTileObj.Properties[0].Value)
                {
                    case 1:
                    {
                        RGM.enemiesInRoom++;
                        RGM.entitiesToBeSpawned.Add(new WalkingEnemy(new Vector2((float)nonTileObj.X, (float)nonTileObj.Y)));
                        break;
                    }
                    
                    case 2:
                    {
                        RGM.enemiesInRoom++;
                        RGM.entitiesToBeSpawned.Add(new TurretEnemy(new Vector2((float)nonTileObj.X, (float)nonTileObj.Y)));
                        break;
                    }
                }
                
            }
            
            // Place door. If there are no enemies, doors shall be open, if there are, they shall be closed.
            placeDoors(RGM.enemiesInRoom <= 0);

        }

        private static void placePlayer(dDirection direction)
        {
            Vector2 playerPos = RGM.Player.position;

            switch (direction)
            {
                case dDirection.up:
                {
                    (playerPos.X, playerPos.Y) = (RGM.roomWidth / 2 * RGM.tileSize, (RGM.roomHeight - 2) * RGM.tileSize);
                    break;
                }
                
                case dDirection.down:
                {
                    (playerPos.X, playerPos.Y) = (RGM.roomWidth / 2 * RGM.tileSize, RGM.tileSize);
                    break;
                }
                    
                case dDirection.left:
                {
                    (playerPos.X, playerPos.Y) = ((RGM.roomWidth - 2) * RGM.tileSize, RGM.roomHeight/2  * RGM.tileSize);  
                    break;
                }
                    
                case dDirection.right:
                {
                    (playerPos.X, playerPos.Y) = (RGM.tileSize, RGM.roomHeight/2  * RGM.tileSize);
                    break;
                }
                    
                case dDirection.center:
                {
                    (playerPos.X, playerPos.Y) = (RGM.roomWidth/2 * RGM.tileSize, RGM.roomHeight/2 * RGM.tileSize);
                    break;
                }
            }    

            RGM.Player.position = playerPos;

        }

        private static void clearRoom()
        {
            
            RGM.entities.RemoveAll(x => !(x is Player));
            RGM.entitiesToBeSpawned.Clear();
            RGM.entitiesToBeKilled.Clear();

            RGM.enemiesInRoom = 0;

        }

        private static void placeWalls()
        {
            float maxX = RGM.tileSize * (RGM.roomWidth - 1);
            float maxY = RGM.tileSize * (RGM.roomHeight - 1);
            Point playerMapPos = RGM.Player.mapPosition;

            int roomHeightWithDoor = RGM.roomHeight - 1;
            int roomWidthWithDoor = RGM.roomWidth - 1;
            
            // Upleft corner
            RGM.entities.Add(new Wall(new Vector2(0, 0), dTileDirection.upleft));

            // Up wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y - 1] == null)
            {
                for (int i = 1; i < RGM.roomWidth - 1; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, 0),
                        dTileDirection.down));
                }
            }
            else
            {
                for (int i = 1; i < roomWidthWithDoor/2; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, 0),
                        dTileDirection.down));
                }
                
                for (int i = roomWidthWithDoor/2 + 1; i < roomWidthWithDoor; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, 0),
                        dTileDirection.down));
                }
            }

            // Upright corner
            RGM.entities.Add(new Wall(new Vector2(maxX, 0), dTileDirection.upright));
            
            // Right wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X + 1, playerMapPos.Y] == null)
            {
                for (int i = 1; i < RGM.roomHeight - 1; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(maxX, RGM.tileSize * i),
                        dTileDirection.left));
                }
            }
            else
            {
                for (int i = 1; i < roomHeightWithDoor/2; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(maxX, RGM.tileSize * i),
                        dTileDirection.left));
                }
                
                for (int i = roomHeightWithDoor/2 + 1; i < roomHeightWithDoor; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(maxX, RGM.tileSize * i),
                        dTileDirection.left));
                }
            }

            // Downright corner
            RGM.entities.Add(new Wall(new Vector2(maxX, maxY), dTileDirection.downright));
            
            // Left wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X - 1, playerMapPos.Y] == null)
            {
                for (int i = 1; i < RGM.roomHeight - 1; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(0, RGM.tileSize * i),
                        dTileDirection.right));
                }
            }
            else
            {
                for (int i = 1; i < roomHeightWithDoor/2; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(0, RGM.tileSize * i),
                        dTileDirection.right));
                }
                
                for (int i = roomHeightWithDoor/2 + 1; i < roomHeightWithDoor; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(0, RGM.tileSize * i),
                        dTileDirection.right));
                }
                
            }

            // Downleft corner
            RGM.entities.Add(new Wall(new Vector2(0, maxY), dTileDirection.downleft));
            
            // Down wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y + 1] == null)
            {
                for (int i = 1; i < RGM.roomWidth - 1; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, maxY),
                        dTileDirection.up));
                }
            }
            else
            {
                for (int i = 1; i < roomWidthWithDoor/2; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, maxY),
                        dTileDirection.up));
                }
                
                for (int i = roomWidthWithDoor/2 + 1; i < roomWidthWithDoor; i++)
                {
                    RGM.entities.Add(new Wall(new Vector2(RGM.tileSize * i, maxY),
                        dTileDirection.up));
                }
            }

        }

        private static void placeDoors(bool open)
        {
            Point playerMapPos = RGM.Player.mapPosition;
            
            // Left
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X - 1, playerMapPos.Y] != null)
            {
                RGM.entities.Add(new Door(new Vector2(0, 5 * RGM.tileSize), dDirection.left, open));
            }
            
            // Right
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X + 1, playerMapPos.Y] != null)
            {
                RGM.entities.Add(new Door(new Vector2(RGM.tileSize * (RGM.roomWidth - 1), 5 * RGM.tileSize), dDirection.right, open));
            }
            
            // Up
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y - 1] != null)
            {
                RGM.entities.Add(new Door(new Vector2(9 * RGM.tileSize, 0), dDirection.up, open));
            }

            // Down
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y + 1] != null)
            {
                RGM.entities.Add(new Door(new Vector2(9 * RGM.tileSize, RGM.tileSize * (RGM.roomHeight - 1)), dDirection.down, open));
            }

        }
        

    }
}