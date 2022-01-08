using System;
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

        public void changeStats(ShooterStats stats)
        {
            this.stats = stats;
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
    
    public class Pistol : Shooter
    {
        public Pistol(ShooterStats shooterStats, dTextureKeys textureKey)
        {
            this.stats = shooterStats;
            this.textureKey = textureKey;
            this.currentReloadTime = shooterStats.reloadTime;
        }

        protected override void shoot()
        {
            GEventHandler.fireEvent(dEvents.shoot);

            RGM.entitiesToBeSpawned.Add(new Bullet(new Vector2(
                targetPosition.X + Util.randomPositiveOrNegative(stats.spread, 0.5f),
                targetPosition.Y + Util.randomPositiveOrNegative(stats.spread, 0.5f)
                ), 
                
                gunPosition, 
                stats.bulletStats, 
                textureKey));
        }
        
    }
    
    public class Shotgun : Shooter
    {
        private const int bulletAmount = 5;
        
        public Shotgun(ShooterStats shooterStats, dTextureKeys textureKey)
        {
            this.stats = shooterStats;
            this.textureKey = textureKey;
            this.currentReloadTime = shooterStats.reloadTime;
        }

        protected override void shoot()
        {
            GEventHandler.fireEvent(dEvents.shoot);

            for (int i = 0; i <= bulletAmount; i++)
            {
                RGM.entitiesToBeSpawned.Add(new Bullet(new Vector2(
                        targetPosition.X + Util.randomPositiveOrNegative(stats.spread, 0.5f),
                        targetPosition.Y + Util.randomPositiveOrNegative(stats.spread, 0.5f)
                    ),

                    gunPosition,
                    stats.bulletStats,
                    textureKey));
            }
        }
        
    }
    
    public class RingeOfBullets : Shooter
    {
        private const int r = 3;
        private const int tr = 8;
        
        public RingeOfBullets(ShooterStats shooterStats, dTextureKeys textureKey)
        {
            this.stats = shooterStats;
            this.textureKey = textureKey;
            this.currentReloadTime = shooterStats.reloadTime;
        }

        protected override void shoot()
        {
            GEventHandler.fireEvent(dEvents.shoot);

            for (double i = 0; i < 2 * Math.PI; i =  i + 0.1)
            {
                RGM.entitiesToBeSpawned.Add(new Bullet(
                    new Vector2((float)(gunPosition.X + tr * Math.Sin(i)), (float)(gunPosition.Y + tr * Math.Cos(i))),
                     new Vector2((float)(gunPosition.X + r * Math.Sin(i)), (float)(gunPosition.Y + r * Math.Cos(i))),
                    stats.bulletStats,
                    textureKey
                ));
                
            }
        }
        
    }
    
    
    
}