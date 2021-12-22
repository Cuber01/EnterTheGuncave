using System;
using System.Collections.Generic;
using System.IO;
using EnterTheGuncave.Entities.Baddies;
using EnterTheGuncave.Entities.Neutrals;
using Microsoft.Xna.Framework;


namespace EnterTheGuncave.General.ContentHandling.Rooms
{
    public static class RoomLoader
    {
        private static readonly List<Room> rooms = new List<Room>();
        

        public static void loadAllRooms()
        {

            // TODO cross platform path
            string path = String.Format("{0}home{0}cubeq{0}RiderProjects{0}EnterTheGuncave{0}EnterTheGuncave{0}Content{0}assets{0}maps{0}", Path.DirectorySeparatorChar);

            string[] files = Directory.GetFiles(path);
                
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
            
        }

        public static void placeWalls()
        {
            EnterTheGuncave.entities.Add(new Wall(new Vector2(0, 0), dTextureKeys.wall_corner1));
        }
        

    }
}