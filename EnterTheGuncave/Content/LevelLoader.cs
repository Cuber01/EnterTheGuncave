using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EnterTheGuncave.Content
{
    public static class LevelLoader
    {
        static List<Level> levels = new List<Level>();

        public static void loadLevel(string path)
        {
            string levelJSON = System.IO.File.ReadAllText(path);

            var levelsMap = LevelsMap.FromJson(levelJSON);
            
        }

    }
}