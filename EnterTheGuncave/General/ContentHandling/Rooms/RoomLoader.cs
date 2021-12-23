using System;
using System.Collections.Generic;
using System.IO;
using EnterTheGuncave.Entities;
using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.Entities.Baddies;
using EnterTheGuncave.Entities.Neutrals;
using EnterTheGuncave.General.DungeonGenerator;
using Microsoft.Xna.Framework;


namespace EnterTheGuncave.General.ContentHandling.Rooms
{
    public static class RoomLoader
    {
        private static readonly List<Room> rooms = new List<Room>();
        public static int roomCount;
        public static bool changingRoom = false;

        public static void loadAllRooms()
        {
            
            // TODO cross platform path
            string path = String.Format("{0}home{0}cubeq{0}RiderProjects{0}EnterTheGuncave{0}EnterTheGuncave{0}Content{0}assets{0}maps{0}", Path.DirectorySeparatorChar);

            string[] files = Directory.GetFiles(path);
            roomCount = files.Length;    
            
            foreach (string file in files)
            {
                rooms.Add(loadRoom(file));
            }
            
        }
        
        private static Room loadRoom(string path)
        {
            string levelJSON = File.ReadAllText(path);

            return Room.FromJson(levelJSON);
        }

        public static void playRoom(int index)
        {
            
            clearRoom();
            placeDoors();
            
            
            (EnterTheGuncave.entities[0].position.X, EnterTheGuncave.entities[0].position.Y) = (50.0f, 50.0f);
            
            Console.WriteLine(index);
            for(int i = 0; i < rooms[index].Layers[0].Data.Length; i++)
            {
                
                if (rooms[index].Layers[0].Data[i] == 0) continue;
                
                int currentCol = i / EnterTheGuncave.roomWidth;
                int colIndex = i % EnterTheGuncave.roomWidth;

                EnterTheGuncave.entitiesToBeSpawned.Add(new Stone(new Vector2(colIndex * EnterTheGuncave.tileSize, currentCol * EnterTheGuncave.tileSize)));
                EnterTheGuncave.currentRoom[colIndex, currentCol] = Int32.MaxValue;
            }

            foreach(var nonTileObj in rooms[index].Layers[1].Objects)
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

            changingRoom = false;

        }

        public static void clearRoom()
        {

            EnterTheGuncave.entities.RemoveAll(x => !(x is Player));
            EnterTheGuncave.entitiesToBeSpawned.Clear();
            EnterTheGuncave.entitiesToBeKilled.Clear();

        }

        public static void placeWalls()
        {
            float maxX = EnterTheGuncave.tileSize * (EnterTheGuncave.roomWidth - 1);
            float maxY = EnterTheGuncave.tileSize * (EnterTheGuncave.roomHeight - 1);
            
            // Upleft corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(0, 0), dTileDirection.upleft));

            // Up wall
            for (int i = 1; i < EnterTheGuncave.roomWidth - 1; i++)
            {
                EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, 0), dTileDirection.down));
            }
            
            // Upright corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, 0), dTileDirection.upright));
            
            // Right wall
            for (int i = 1; i < EnterTheGuncave.roomHeight - 1; i++)
            {
                EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, EnterTheGuncave.tileSize * i), dTileDirection.left));
            }
            
            // Downright corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(maxX, maxY), dTileDirection.downright));
            
            // Left wall
            for (int i = 1; i < EnterTheGuncave.roomHeight - 1; i++)
            {
                EnterTheGuncave.entities.Add(new Wall(new Vector2(0, EnterTheGuncave.tileSize * i), dTileDirection.right));
            }
            
            // Downleft corner
            EnterTheGuncave.entities.Add(new Wall(new Vector2(0, maxY), dTileDirection.downleft));
            
            for (int i = 1; i < EnterTheGuncave.roomWidth - 1; i++)
            {
                EnterTheGuncave.entities.Add(new Wall(new Vector2(EnterTheGuncave.tileSize * i, maxY), dTileDirection.up));
            }

        }

        public static void placeDoors()
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