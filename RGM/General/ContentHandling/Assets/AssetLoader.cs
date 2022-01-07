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
        public static readonly Dictionary<dFontKeys, SpriteFont> fonts = new Dictionary<dFontKeys, SpriteFont>();
        
        private readonly ContentManager contentManager;

        public AssetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }
        
        public void loadTextures()
        {
            textures.Add(dTextureKeys.player,          contentManager.Load<Texture2D>("assets/images/player"));
            textures.Add(dTextureKeys.enemy,           contentManager.Load<Texture2D>("assets/images/enemy"));
            textures.Add(dTextureKeys.door,            contentManager.Load<Texture2D>("assets/images/arrow"));
            textures.Add(dTextureKeys.stone,           contentManager.Load<Texture2D>("assets/images/stone"));
            textures.Add(dTextureKeys.player_bullet,   contentManager.Load<Texture2D>("assets/images/player_bullet"));
            textures.Add(dTextureKeys.enemy_bullet,    contentManager.Load<Texture2D>("assets/images/enemy_bullet"));
            textures.Add(dTextureKeys.tiles1,          contentManager.Load<Texture2D>("assets/images/tiles1"));
            textures.Add(dTextureKeys.pedestal,        contentManager.Load<Texture2D>("assets/images/pedestal"));
            textures.Add(dTextureKeys.enemy_turret,    contentManager.Load<Texture2D>("assets/images/enemy_turret"));
            

            
            textures.Add(dTextureKeys.arrow,            contentManager.Load<Texture2D>("assets/images/items/arrow"));
            textures.Add(dTextureKeys.blood_chalice,    contentManager.Load<Texture2D>("assets/images/items/blood_chalice"));
            textures.Add(dTextureKeys.determination,    contentManager.Load<Texture2D>("assets/images/items/determination"));
            textures.Add(dTextureKeys.knife,            contentManager.Load<Texture2D>("assets/images/items/knife"));
            textures.Add(dTextureKeys.medkit,           contentManager.Load<Texture2D>("assets/images/items/medkit"));
            textures.Add(dTextureKeys.shotgun,          contentManager.Load<Texture2D>("assets/images/items/shotgun"));
            textures.Add(dTextureKeys.gun_ring,         contentManager.Load<Texture2D>("assets/images/items/gun_ring"));
        }
        
        public void loadSounds()
        {
            sfx.Add(dSoundKeys.enemy_die,   contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_die"));
            sfx.Add(dSoundKeys.enemy_hurt,  contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_hurt"));
            sfx.Add(dSoundKeys.enemy_hurt2, contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/enemy_hurt2"));
            sfx.Add(dSoundKeys.shoot,       contentManager.Load<SoundEffect>("assets/sounds/sfx/wav/shoot"));
        }

        public void loadFonts()
        {
            fonts.Add(dFontKeys.pico8_big, contentManager.Load<SpriteFont>("assets/fonts/PICO8-big"));
            fonts.Add(dFontKeys.pico8_small, contentManager.Load<SpriteFont>("assets/fonts/PICO8-small"));
        }
    }
}