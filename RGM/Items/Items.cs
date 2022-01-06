using Microsoft.Xna.Framework;
using RGM.General.EventHandling;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;
using RGM.General.Graphics;

namespace RGM.Items
{

    // Heal every 10 kills
    public class BloodChalice : BaseItem
    {
        private int enemiesKilled;

        public BloodChalice() : base(dItems.blood_chalice, AssetLoader.textures[dTextureKeys.blood_chalice])
        {
            this.name = "Blood Chalice";
            this.description = "No one heals himself by hurting others. Unless you're a vampire.";
        }

        public override void pickedUp()
        {
            base.pickedUp();
            
            GEventHandler.subscribe(activate, dEvents.enemyKilled);
        }

        public override void activate(dEvents e = dEvents.none)
        {
            enemiesKilled++;

            if (enemiesKilled >= 10)
            {
                enemiesKilled = 0;
                ItemEffects.modifyPlayerHealth(1);
            }

        }
    }
    
    // Health + 5
    public class Medkit : BaseItem
    {
        public Medkit() : base(dItems.medkit, AssetLoader.textures[dTextureKeys.medkit])
        {
            this.name = "Medkit";
            this.description = "Medic!! Gives you health.";
        }

        public override void pickedUp()
        {
            base.pickedUp();

            activate();
        }

        public override void activate(dEvents e = dEvents.none)
        {
            ItemEffects.modifyPlayerHealth(5);
        }
    }
    
}