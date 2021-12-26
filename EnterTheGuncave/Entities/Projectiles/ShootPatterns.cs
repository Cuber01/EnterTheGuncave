using System.Data;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Projectiles
{
    public class Shooter
    {
        protected ShooterStats shooterStats;

        private Vector2 gunPosition;
        private Vector2 targetPosition;
        
        private int currentReloadTime;
        protected int reloadTime;
        protected int accuracy;

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
        
        public void update(Vector2 gunPos, Vector2 targetPos)
        {
            this.gunPosition = gunPos;
            this.targetPosition = targetPos;
            
            // Can we shoot?
            if (reload())
            {
                // Yes we can!
                shoot();
            }
        }
        
        protected virtual void shoot() {}
    }
    
    public class PistolShooter : Shooter
    {
        public PistolShooter(int reloadTime, int accuracy, BulletStats bulletStats)
        {
            this.reloadTime  = reloadTime;
            this.accuracy    = accuracy;
            this.bulletStats = bulletStats;
        }

        protected override void shoot()
        {
            EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(targetPosition, gunPosition, bulletStats));
        }
        
    }
}