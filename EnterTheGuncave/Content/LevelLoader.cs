using System;
using Newtonsoft.Json;
using QuickType;

namespace EnterTheGuncave.Content
{
    public static class LevelLoader
    {
        public static void loadLevel(string path)
        {
            string levelJSON = System.IO.File.ReadAllText(path);

            var levelsMap = LevelsMap.FromJson(levelJSON);
            
        }

    }
}