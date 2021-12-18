using System;
using System.Collections.Generic;
using System.IO;

namespace EnterTheGuncave.General.ContentHandling.Rooms
{
    public static class RoomLoader
    {
        public static List<Room> rooms = new List<Room>();

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

    }
}