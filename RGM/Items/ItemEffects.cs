using System;
using RGM.Entities.Baddies;
using RGM.Entities.Projectiles;
using RGM.General.ContentHandling.Assets;

namespace RGM.Items
{
    public static class ItemEffects
    {
        public static void modifyPlayerHealth(int amount)
        {
            RGM.Player.stats.hitpoints += amount;
        }

        public static void changePlayerShooter(Type shooter)
        {
            RGM.Player.shooter = Activator.CreateInstance(shooter, RGM.Player.shooterStats, dTextureKeys.player_bullet) as Shooter;
        }

        public static void modifyPlayerStats(EntityStats statChange)
        {
            EntityStats stats = RGM.Player.stats;
            
            stats.speed       += statChange.speed;
            stats.damage      += statChange.damage;
            stats.hitpoints   += statChange.hitpoints;

            stats.penetration += statChange.penetration;
            stats.bulletSpeed += statChange.bulletSpeed;
            stats.range       += statChange.range;
            stats.reloadTime  += statChange.reloadTime;
            stats.spread      += statChange.spread;
            
            RGM.Player.shooter.changeStats(new ShooterStats(
                stats.reloadTime,
                stats.spread, 
            
                new BulletStats(
                    stats.bulletSpeed,
                    stats.penetration,
                    stats.range,
                    stats.damage,
                    RGM.Player.team
                )  
            ));
            
        }


    }
}