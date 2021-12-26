using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Baddies
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
            
            this.texture = AssetLoader.textures[dTextureKeys.enemy_turret];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.shooter = new PistolShooter(shooterStats);
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            this.collider = new Hitbox(position, myWidth, myHeight);

            stats.speed = 0;
            stats.heartDamage = 1;
            stats.hitpoints = 10;
        }

        public override void update()
        {
            Vector2 target = new Vector2(EnterTheGuncave.entities[0].position.X, EnterTheGuncave.entities[0].position.Y);

            shooter.update(position, target);
        }
        

        /* ------------------- DAMAGE ------------------- */

        public override void takeDamage(int dmg)
        {
            stats.hitpoints = stats.hitpoints - dmg;
            this.texture = AssetLoader.textures[dTextureKeys.player];
        }
        
    }
}