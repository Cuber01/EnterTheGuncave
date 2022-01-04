using Microsoft.Xna.Framework;
using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;

namespace RGM.Entities.Projectiles
{
    public class Shooter
    {
        protected ShooterStats stats;
        protected dTextureKeys textureKey;

        protected Vector2 gunPosition;
        protected Vector2 targetPosition;
        
        protected int currentReloadTime;
        
        public void update(Vector2 gunPos, Vector2 targetPos, bool doWeShoot)
        {
            this.gunPosition = gunPos;
            this.targetPosition = targetPos;
            
            // Can we shoot?
            if (reload() && doWeShoot)
            {
                // Reset reload
                currentReloadTime = stats.reloadTime;
                
                // Yes, we can!
                shoot();
            }
        }
        
        // Returns whether we're ready to shoot or not. Not a very nice name but I couldn't find a better one.
        private bool reload()
        {
            currentReloadTime--;

            if (currentReloadTime <= 0)
            {
                return true;
            }

            return false;

        }
        
        protected virtual void shoot() {}
    }
    
    public class PistolShooter : Shooter
    {
        public PistolShooter(ShooterStats shooterStats, dTextureKeys textureKey)
        {
            this.stats = shooterStats;
            this.textureKey = textureKey;
            this.currentReloadTime = shooterStats.reloadTime;
        }

        protected override void shoot()
        {
            GEventHandler.fireEvent(dEvents.shoot);

            RGM.entitiesToBeSpawned.Add(new Bullet(targetPosition, gunPosition, stats.bulletStats, textureKey));
        }
        
    }
}