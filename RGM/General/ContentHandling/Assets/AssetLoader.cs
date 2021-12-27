using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RGM.General.ContentHandling.Assets
{
    public enum dTextureKeys
    {
        player,
        enemy,
        enemy_turret,
        arrow,
        stone,
        bullet,
        tiles1,
        pedestal
    }

    public class AssetLoader
    {
        public static readonly Dictionary<dTextureKeys, Texture2D> textures = new Dictionary<dTextureKeys, Texture2D>();
        private readonly ContentManager contentManager;

        public AssetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        
        public void loadTextures()
        {
            textures.Add(dTextureKeys.player,   contentManager.Load<Texture2D>("assets/images/player"));
            textures.Add(dTextureKeys.enemy,    contentManager.Load<Texture2D>("assets/images/enemy"));
            textures.Add(dTextureKeys.arrow,    contentManager.Load<Texture2D>("assets/images/arrow"));
            textures.Add(dTextureKeys.stone,    contentManager.Load<Texture2D>("assets/images/stone"));
            textures.Add(dTextureKeys.bullet,   contentManager.Load<Texture2D>("assets/images/bullet"));
            textures.Add(dTextureKeys.tiles1,   contentManager.Load<Texture2D>("assets/images/tiles1"));
            textures.Add(dTextureKeys.pedestal, contentManager.Load<Texture2D>("assets/images/pedestal"));
            textures.Add(dTextureKeys.enemy_turret, contentManager.Load<Texture2D>("assets/images/enemy_turret"));
        }
    }
}