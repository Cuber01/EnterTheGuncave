using Microsoft.Xna.Framework;
using RGM.General.EventHandling;
using Microsoft.Xna.Framework.Graphics;
using RGM.Entities.Baddies;
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
    
    public class Arrow : BaseItem
    {
        public Arrow() : base(dItems.arrow, AssetLoader.textures[dTextureKeys.arrow])
        {
            this.name = "Arrow";
            this.description = "Increased bullet penetration.";
        }

        public override void pickedUp()
        {
            base.pickedUp();

            activate();
        }

        public override void activate(dEvents e = dEvents.none)
        {
            ItemEffects.modifyPlayerStats(new EntityStats(
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                0
            ));
        }
    }
    
    // +1 damage
    public class Knife : BaseItem
    {
        public Knife() : base(dItems.knife, AssetLoader.textures[dTextureKeys.knife])
        {
            this.name = "Knife";
            this.description = "Increased damage.";
        }

        public override void pickedUp()
        {
            base.pickedUp();

            activate();
        }

        public override void activate(dEvents e = dEvents.none)
        {
            ItemEffects.modifyPlayerStats(new EntityStats(
                0,
                1,
                0,
                0,
                0,
                0,
                0,
                0
            ));
        }
    }
    
    // +50 range
    public class Determination : BaseItem
    {
        public Determination() : base(dItems.determination, AssetLoader.textures[dTextureKeys.determination])
        {
            this.name = "Determination";
            this.description = "Makes your bullets determined and live longer.";
        }

        public override void pickedUp()
        {
            base.pickedUp();

            activate();
        }

        public override void activate(dEvents e = dEvents.none)
        {
            ItemEffects.modifyPlayerStats(new EntityStats(
                0,
                0,
                0,
                0,
                50,
                0,
                0,
                0
            ));
        }
    }
    
}