using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RGM.General.ContentHandling.Assets
{

    public class AssetLoader
    {
        public static readonly Dictionary<dTextureKeys, Texture2D> textures = new Dictionary<dTextureKeys, Texture2D>();
        public static readonly Dictionary<dSoundKeys, SoundEffect> sfx = new Dictionary<dSoundKeys, SoundEffect>();
        
        private readonly ContentManager contentManager;

        public AssetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        
        public void loadTextures()
        {
            textures.Add(dTextureKeys.player,   contentManager.Load<Texture2D>("assets/images/player"));
            textures.Add(dTextureKeys.enemy,    contentManager.Load<Texture2D>("assets/images/enemy"));
            textures.Add(dTextureKeys.door,    contentManager.Load<Texture2D>("assets/images/arrow"));
            textures.Add(dTextureKeys.stone,    contentManager.Load<Texture2D>("assets/images/stone"));
            textures.Add(dTextureKeys.bullet,   contentManager.Load<Texture2D>("assets/images/bullet"));
            textures.Add(dTextureKeys.tiles1,   contentManager.Load<Texture2D>("assets/images/tiles1"));
            textures.Add(dTextureKeys.pedestal, contentManager.Load<Texture2D>("assets/images/pedestal"));
            textures.Add(dTextureKeys.enemy_turret, contentManager.Load<Texture2D>("assets/images/enemy_turret"));
        }
        
        public void loadSounds()
        {
            sfx.Add(dSoundKeys.enemy_die, contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_die"));
            sfx.Add(dSoundKeys.enemy_hurt, contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_hurt"));
            sfx.Add(dSoundKeys.enemy_hurt2, contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_hurt2"));
            sfx.Add(dSoundKeys.shoot, contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/shoot"));
        }
    }
}