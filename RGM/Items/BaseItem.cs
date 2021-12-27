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

        public Texture2D texture;
        private dItems item;
        
        public BaseItem(dItems item, Texture2D texture)
        {
            this.item = item;
            this.texture = texture;
        }

        public virtual void activate() { }
        public virtual void pickedUp() { }
    }

    // Heal every 10 kills
    public class VampireMedkit : BaseItem
    {
        private int enemiesKilled;
        
        
        public VampireMedkit() : base(dItems.vampire_medkit, AssetLoader.textures[dTextureKeys.player])
        {
            this.name = "Vampire Bullets";
            this.description = "No one heals himself by hurting others. Unless you're a vampire.";
        }

        public override void pickedUp()
        {
            ItemEventHandler.subscribe(this, dEvents.enemyKilled);
        }

        public override void activate()
        {
            enemiesKilled++;

            if (enemiesKilled >= 10)
            {
                enemiesKilled = 0;
                ItemEffects.modifyPlayerHealth(1);
            }
            
        }
    }
    
}