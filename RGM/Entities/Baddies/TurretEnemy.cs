using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Animation;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;

namespace RGM.Entities.Baddies
{
    public class TurretEnemy : Entity
    {
        
        private readonly SimpleAnimator animator;
        private new readonly Pistol shooter;
        
        private new readonly ShooterStats shooterStats;

        public TurretEnemy(Vector2 position)
        {
            // Position
            this.position = position;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            // Team
            this.team = dTeam.baddies;

            // Drawing
            this.texture  = AssetLoader.textures[dTextureKeys.enemy_turret];
            this.myWidth  = 8;
            this.myHeight = 8;

            this.animator = new SimpleAnimator(texture, animation);

            // Collision            
            this.collider = new Hitbox(position, myWidth, myHeight);

            // Stats
            stats.speed = 0;
            stats.damage = 1;
            stats.hitpoints = 5;

            stats.penetration = 1;
            stats.bulletSpeed = 1;
            stats.range = 200;
            stats.reloadTime = 260;
            stats.spread = 1;
            
            this.shooterStats = new ShooterStats(
                stats.reloadTime,
                stats.spread, 
            
                new BulletStats(
                    stats.bulletSpeed,
                    stats.penetration,
                    stats.range,
                    stats.damage,
                    team
                )  
            );
            
            this.shooter = new Pistol(shooterStats, dTextureKeys.enemy_bullet);

        }


        
        public override void update()
        {
            Vector2 target = new Vector2(RGM.Player.position.X, RGM.Player.position.Y);

            shooter.update(position, target, true);
        }

        public override void draw()
        {
            animator.draw(position);
        }


        /* ------------------- DAMAGE ------------------- */

        public override void takeDamage(int dmg)
        {
            stats.hitpoints = stats.hitpoints - dmg;
            checkDeath();
        }

        private void checkDeath()
        {
            
            if(stats.hitpoints <= 0)
            {
                GEventHandler.fireEvent(dEvents.enemyKilled);
                dead = true;
                
                return;
            }
            
            GEventHandler.fireEvent(dEvents.enemyHurt);
            
        }
        
        /* ----------------- ANIMATION ------------------- */
        
        private static readonly Dictionary<Rectangle, int> animation = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 0, 8, 10), 50
            },
           
            {
                new Rectangle(8, 0, 8, 10), 50
            },
            
            {
                new Rectangle(16, 0, 8, 10), 50
            },

            {
                new Rectangle(24, 0, 8, 10), 50
            },
            
            {
                new Rectangle(32, 0, 8, 10), 5
            },
            
            {
                new Rectangle(40, 0, 8, 10), 5
            },
            
            {
                new Rectangle(48, 0, 8, 10), 5
            },
            
            {
                new Rectangle(56, 0, 8, 10), 50
            },
            
        };
        
    }
}