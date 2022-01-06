using Microsoft.Xna.Framework;
using RGM.General.EventHandling;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;
using RGM.General.Graphics;

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

        public virtual void activate(dEvents e) { }

        public virtual void pickedUp()
        {
            FontRenderer.textQueue.Add(new textInfo(name,        200, new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 300), dFontKeys.pico8_big));
            FontRenderer.textQueue.Add(new textInfo(description, 200, new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 250), dFontKeys.pico8_small));
        }
    }

    // Heal every 10 kills
    public class BloodChalice : BaseItem
    {
        private int enemiesKilled;

        public BloodChalice() : base(dItems.vampire_medkit, AssetLoader.textures[dTextureKeys.player])
        {
            this.name = "Blood Chalice";
            this.description = "No one heals himself by hurting others. Unless you're a vampire.";
        }

        public override void pickedUp()
        {
            base.pickedUp();
            
            GEventHandler.subscribe(activate, dEvents.enemyKilled);
        }

        public override void activate(dEvents e)
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