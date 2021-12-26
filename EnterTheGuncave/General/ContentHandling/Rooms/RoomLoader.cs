using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.Entities.Baddies;
using EnterTheGuncave.Entities.Neutrals;
using EnterTheGuncave.General.DungeonGenerator;
using Microsoft.Xna.Framework;


namespace EnterTheGuncave.General.ContentHandling.Rooms
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
            
            // TODO solve this and it should be OK
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
            // Clear the current room, place doors and walls in the new one
            clearRoom();
            placeDoors();
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
                
                int currentCol = i / EnterTheGuncave.roomWidth;
                int colIndex = i % EnterTheGuncave.roomWidth;

                switch (allRoomsOfType[index].Layers[0].Data[i])
                {
                    case 1:
                        EnterTheGuncave.entitiesToBeSpawned.Add(new Stone(
                            new Vector2(colIndex * EnterTheGuncave.tileSize, currentCol * EnterTheGuncave.tileSize)));
                        break;
                    
                    case 2: EnterTheGuncave.entitiesToBeSpawned.Add(new Pedestal
                            (new Vector2(colIndex * EnterTheGuncave.tileSize, currentCol * EnterTheGuncave.tileSize)));
                        break;
                    
                    case 4: EnterTheGuncave.entitiesToBeSpawned.Add(new WalkingEnemy
                            (new Vector2(colIndex * EnterTheGuncave.tileSize, currentCol * EnterTheGuncave.tileSize)));
                        break;
                    
                    case 5:
                        EnterTheGuncave.entitiesToBeSpawned.Add(new TurretEnemy
                            (new Vector2(colIndex * EnterTheGuncave.tileSize, currentCol * EnterTheGuncave.tileSize)));
                        break;                
                    
                }


                EnterTheGuncave.currentRoom[colIndex, currentCol] = Int32.MaxValue;
            }

            foreach(var nonTileObj in allRoomsOfType[index].Layers[1].Objects)
            {
                
                switch (nonTileObj.Properties[0].Value)
                {
                    case 1:
                    {
                        EnterTheGuncave.entitiesToBeSpawned.Add(new WalkingEnemy(new Vector2((float)nonTileObj.X, (float)nonTileObj.Y)));
                        break;
                    }
                }
                
            }

        }

        private static void placePlayer(dDirection direction)
        {
            Vector2 playerPos = EnterTheGuncave.entities[0].position;
            float offset = 0.0f; // TODO check
            
            switch (direction)
            {
                case dDirection.up:
                {
                    (playerPos.X, playerPos.Y) = (EnterTheGuncave.roomWidth / 2 * EnterTheGuncave.tileSize, (EnterTheGuncave.roomHeight - 2) * EnterTheGuncave.tileSize - offset);
                    break;
                }
                
                case dDirection.down:
                {
                    (playerPos.X, playerPos.Y) = (EnterTheGuncave.roomWidth / 2 * EnterTheGuncave.tileSize, EnterTheGuncave.tileSize + offset);
                    break;
                }
                    
                case dDirection.left:
                {
                    (playerPos.X, playerPos.Y) = ((EnterTheGuncave.roomWidth - 2) * EnterTheGuncave.tileSize - offset, EnterTheGuncave.roomHeight/2  * EnterTheGuncave.tileSize);  
                    break;
                }
                    
                case dDirection.right:
                {
                    (playerPos.X, playerPos.Y) = (EnterTheGuncave.tileSize, EnterTheGuncave.roomHeight/2  * EnterTheGuncave.tileSize + offset);
                    break;
                }
                    
                case dDirection.center:
                {
                    (playerPos.X, playerPos.Y) = (EnterTheGuncave.roomWidth/2 * EnterTheGuncave.tileSize, EnterTheGuncave.roomHeight/2 * EnterTheGuncave.tileSize);
                    break;
                }
            }    

            EnterTheGuncave.entities[0].position = playerPos;

        }

        private static void clearRoom()
        {
            
            EnterTheGuncave.entities.RemoveAll(x => !(x is Player));
            EnterTheGuncave.entitiesToBeSpawned.Clear();
            EnterTheGuncave.entitiesToBeKilled.Clear();

        }

        private static void placeWalls()
        {
            float maxX = EnterTheGuncave.tileSize * (EnterTheGuncave.roomWidth - 1);
            float maxY = EnterTheGuncave.tileSize * (EnterTheGuncave.roomHeight - 1);
            Point playerMapPos = EnterTheGuncave.entities[0].mapPosition;

            int roomHeightWithDoor = EnterTheGuncave.roomHeight - 1;
            int roomWidthWithDoor = EnterTheGuncave.roomWidth - 1;
            
            // Upleft corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(0, 0), dTileDirection.upleft));

            // Up wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y - 1] == null)
            {
                for (int i = 1; i < EnterTheGuncave.roomWidth - 1; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, 0),
                        dTileDirection.down));
                }
            }
            else
            {
                for (int i = 1; i < roomWidthWithDoor/2; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, 0),
                        dTileDirection.down));
                }
                
                for (int i = roomWidthWithDoor/2 + 1; i < roomWidthWithDoor; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, 0),
                        dTileDirection.down));
                }
            }

            // Upright corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, 0), dTileDirection.upright));
            
            // Right wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X + 1, playerMapPos.Y] == null)
            {
                for (int i = 1; i < EnterTheGuncave.roomHeight - 1; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, EnterTheGuncave.tileSize * i),
                        dTileDirection.left));
                }
            }
            else
            {
                for (int i = 1; i < roomHeightWithDoor/2; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, EnterTheGuncave.tileSize * i),
                        dTileDirection.left));
                }
                
                for (int i = roomHeightWithDoor/2 + 1; i < roomHeightWithDoor; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, EnterTheGuncave.tileSize * i),
                        dTileDirection.left));
                }
            }

            // Downright corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, maxY), dTileDirection.downright));
            
            // Left wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X - 1, playerMapPos.Y] == null)
            {
                for (int i = 1; i < EnterTheGuncave.roomHeight - 1; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(0, EnterTheGuncave.tileSize * i),
                        dTileDirection.right));
                }
            }
            else
            {
                for (int i = 1; i < roomHeightWithDoor/2; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(0, EnterTheGuncave.tileSize * i),
                        dTileDirection.right));
                }
                
                for (int i = roomHeightWithDoor/2 + 1; i < roomHeightWithDoor; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(0, EnterTheGuncave.tileSize * i),
                        dTileDirection.right));
                }
                
            }

            // Downleft corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(0, maxY), dTileDirection.downleft));
            
            // Down wall
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y + 1] == null)
            {
                for (int i = 1; i < EnterTheGuncave.roomWidth - 1; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, maxY),
                        dTileDirection.up));
                }
            }
            else
            {
                for (int i = 1; i < roomWidthWithDoor/2; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, maxY),
                        dTileDirection.up));
                }
                
                for (int i = roomWidthWithDoor/2 + 1; i < roomWidthWithDoor; i++)
                {
                    EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, maxY),
                        dTileDirection.up));
                }
            }

        }

        private static void placeDoors()
        {
            Point playerMapPos = EnterTheGuncave.entities[0].mapPosition;
            
            // Left
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X - 1, playerMapPos.Y] != null)
            {
                EnterTheGuncave.entities.Add(new Door(new Vector2(0, 5 * EnterTheGuncave.tileSize), dDirection.left));
            }
            
            // Right
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X + 1, playerMapPos.Y] != null)
            {
                EnterTheGuncave.entities.Add(new Door(new Vector2(EnterTheGuncave.tileSize * (EnterTheGuncave.roomWidth - 1), 5 * EnterTheGuncave.tileSize), dDirection.right));
            }
            
            // Up
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y - 1] != null)
            {
                EnterTheGuncave.entities.Add(new Door(new Vector2(9 * EnterTheGuncave.tileSize, 0), dDirection.up));
            }

            // Down
            if (DungeonGenerator.DungeonGenerator.floorMap[playerMapPos.X, playerMapPos.Y + 1] != null)
            {
                EnterTheGuncave.entities.Add(new Door(new Vector2(9 * EnterTheGuncave.tileSize, EnterTheGuncave.tileSize * (EnterTheGuncave.roomHeight - 1)), dDirection.down));
            }

        }
        

    }
}