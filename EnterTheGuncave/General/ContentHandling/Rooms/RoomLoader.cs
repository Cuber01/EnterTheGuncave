using System;
using System.Collections.Generic;
using System.IO;
using EnterTheGuncave.Entities.Neutrals;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.ContentHandling.Rooms
{
    public static class RoomLoader
    {
        private static List<Room> rooms = new List<Room>();
        private static int realRoomWidth = EnterTheGuncave.roomWidth - 2;
        private static int realRoomHeight = EnterTheGuncave.roomHeight - 2;
        
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

        public static void playRoom(int index)
        {
            foreach (var l in rooms[index].Layers[0].Data )
            {
                if (l != 0)
                {
                    EnterTheGuncave.entitiesToBeSpawned.Add(new Stone(new Vector2(l * EnterTheGuncave.tileSize, 0)));
                }
            }
        }
        
        
        private static Room loadRoom(string path)
        {
            string levelJSON = File.ReadAllText(path);

            return Room.FromJson(levelJSON);
        }

    }
}