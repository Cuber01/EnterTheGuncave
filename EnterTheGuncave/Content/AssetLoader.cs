using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave.Content
{
    public class AssetLoader
    {
        public static readonly Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private readonly ContentManager contentManager;

        public AssetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        
        public void loadTextures()
        {
            textures.Add("player", contentManager.Load<Texture2D>("assets/images/player"));
            textures.Add("enemy", contentManager.Load<Texture2D>("assets/images/enemy"));
            textures.Add("arrow", contentManager.Load<Texture2D>("assets/images/arrow"));
            textures.Add("stone", contentManager.Load<Texture2D>("assets/images/stone"));
            textures.Add("bullet", contentManager.Load<Texture2D>("assets/images/bullet"));
        }
    }
}