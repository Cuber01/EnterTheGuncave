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
        
        private readonly SimpleAnimator _simpleAnimator;
        private EnemyStats stats;
        private readonly PistolShooter shooter;

        private readonly ShooterStats shooterStats = new ShooterStats(
            260,
              1, 
    new BulletStats(
                1,
            2,
               500,
               1,
                      dTeam.baddies
                ));

        public TurretEnemy(Vector2 position)
        {
            this.position = position;
            this.team = dTeam.baddies;

            this.texture  = AssetLoader.textures[dTextureKeys.enemy_turret];
            this.myWidth  = 8;
            this.myHeight = 8;

            this._simpleAnimator = new SimpleAnimator(texture, animation);

            this.shooter = new PistolShooter(shooterStats);
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            this.collider = new Hitbox(position, myWidth, myHeight);

            stats.speed = 0;
            stats.heartDamage = 1;
            stats.hitpoints = 10;
        }
        
        public override void update()
        {
            Vector2 target = new Vector2(RGM.Player.position.X, RGM.Player.position.Y);

            shooter.update(position, target, true);
        }

        public override void draw()
        {
            _simpleAnimator.draw(position);
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