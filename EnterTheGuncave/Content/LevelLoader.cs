using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EnterTheGuncave.Content
{
    public static class LevelLoader
    {
        public static List<LevelsMap> levels = new List<LevelsMap>();

        public static void loadAllLevels()
        {

            // TODO cross platform path
            string path = String.Format("{0}home{0}cubeq{0}RiderProjects{0}EnterTheGuncave{0}EnterTheGuncave{0}Content{0}assets{0}maps{0}", Path.DirectorySeparatorChar);

            string[] files = Directory.GetFiles(path);
                
            foreach (string file in files)
            {
                levels.Add(loadLevel(file));
            }
            
        }
        
        
        private static LevelsMap loadLevel(string path)
        {
            string levelJSON = File.ReadAllText(path);

            return LevelsMap.FromJson(levelJSON);
        }

    }
}