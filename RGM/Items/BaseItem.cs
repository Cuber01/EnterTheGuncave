using System;
using RGM.General.EventHandling;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;

namespace RGM.Items
{
    
    public class BaseItem
    {
        public string name;
        public string description;

        private Texture2D texture;
        private dItems item;
        
        public BaseItem(dItems item, Texture2D texture)
        {
            this.item = item;
            this.texture = texture;
        }

        public virtual void activate() { }
    }

    public class VampireMedkit : BaseItem
    {
        public VampireMedkit() : base(dItems.vampire_medkit, AssetLoader.textures[dTextureKeys.player])
        {
            this.name = "Vampire's Medkit";
            this.description = "No One Heals Himself by Hurting Others. Unless you're a vampire.";

            GEventHandler.subscribe(this, dEvents.enemyKilled);
        }

        public override void activate()
        {
            ItemEffects.modifyPlayerHealth(1);
        }
    }
    
}