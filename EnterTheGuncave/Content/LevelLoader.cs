using Newtonsoft.Json;

namespace EnterTheGuncave.Content
{
    public class LevelLoader
    {
        
        public class Movie
        {
            public string Name;
        }

        static string json = @"{
            'Name': 'Bad Boys',
            'ReleaseDate': '1995-4-7T00:00:00',
            
            'Genres': [
                'Action',
                'Comedy'
            ]
        }";

        static Movie m = JsonConvert.DeserializeObject<Movie>(json);

        string name = m.Name;

    }
}