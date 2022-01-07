using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RGM.General.ContentHandling.Rooms
{
    public partial class Room
    {

        [JsonProperty("layers")]
        public Layer[] Layers { get; set; }

        public bool setItems = false;

        // [JsonProperty("compressionlevel")]
        // public long Compressionlevel { get; set; }

        // [JsonProperty("editorsettings")]
        // public Editorsettings Editorsettings { get; set; }

        // [JsonProperty("height")]
        // public long Height { get; set; }

        // [JsonProperty("infinite")]
        // public bool Infinite { get; set; }

        // [JsonProperty("nextlayerid")]
        // public long Nextlayerid { get; set; }

        // [JsonProperty("nextobjectid")]
        // public long Nextobjectid { get; set; }

        // [JsonProperty("orientation")]
        // public string Orientation { get; set; }

        // [JsonProperty("renderorder")]
        // public string Renderorder { get; set; }

        // [JsonProperty("tiledversion")]
        // public string Tiledversion { get; set; }

        // [JsonProperty("tileheight")]
        // public long Tileheight { get; set; }

        // [JsonProperty("tilesets")]
        // public Tileset[] Tilesets { get; set; }

        // [JsonProperty("tilewidth")]
        // public long Tilewidth { get; set; }

        // [JsonProperty("type")]
        // public string Type { get; set; }

        // [JsonProperty("version")]
        // public string Version { get; set; }

        // [JsonProperty("width")]
        // public long Width { get; set; }
    }
    
    //
    // public partial class Editorsettings
    // {
    //     [JsonProperty("export")]
    //     public Export Export { get; set; }
    // }
    //
    // public partial class Export
    // {
    //     [JsonProperty("format")]
    //     public string Format { get; set; }
    //
    //     [JsonProperty("target")]
    //     public string Target { get; set; }
    // }

    public class Layer
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public long[] Data { get; set; }
        
        [JsonProperty("objects", NullValueHandling = NullValueHandling.Ignore)]
        public Object[] Objects { get; set; }
        
        // [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        // public long? Height { get; set; }
        
        // [JsonProperty("id")]
        // public long Id { get; set; }
        
        // [JsonProperty("name")]
        // public string Name { get; set; }
        
        // [JsonProperty("opacity")]
        // public long Opacity { get; set; }
        
        // [JsonProperty("type")]
        // public string Type { get; set; }
        
        // [JsonProperty("visible")]
        // public bool Visible { get; set; }
        
        // [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        // public long? Width { get; set; }
        
        // [JsonProperty("x")]
        // public long X { get; set; }
        
        // [JsonProperty("y")]
        // public long Y { get; set; }
        
        // [JsonProperty("draworder", NullValueHandling = NullValueHandling.Ignore)]
        // public string Draworder { get; set; }

    }

    public class Object
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
        
        [JsonProperty("properties")]
        public Property[] Properties { get; set; }
        
        // [JsonProperty("name")]
        // public string Name { get; set; }
        
        // [JsonProperty("point")]
        // public bool Point { get; set; }

        // [JsonProperty("rotation")]
        // public long Rotation { get; set; }
        
        // [JsonProperty("type")]
        // public string Type { get; set; }
        
        // [JsonProperty("visible")]
        // public bool Visible { get; set; }
        
        // [JsonProperty("width")]
        // public long Width { get; set; }
        //
        // [JsonProperty("height")]
        // public long Height { get; set; }
        
    }

    public class Property
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        // [JsonProperty("type")]
        // public string Type { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public class Tileset
    {
        [JsonProperty("firstgid")]
        public long Firstgid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public partial class Room
    {
        public static Room FromJson(string json) => JsonConvert.DeserializeObject<Room>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Room self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

