using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;

namespace RGM.Entities.Baddies
{
    public class TurretEnemy : Entity
    {
        private EnemyStats stats;
        private readonly PistolShooter shooter;

        private readonly ShooterStats shooterStats = new ShooterStats(
            100,
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
            this.isDangerous = true;
            
            this.texture = AssetLoader.textures[dTextureKeys.enemy_turret];
            this.myWidth  = texture.Width  / RGM.scale;
            this.myHeight = texture.Height / RGM.scale;

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
                EventHandler.fireEvent(dEvents.enemyKilled);
                dead = true;
            }
            
        }
        
    }
}