using System;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

// TODO pathfinding needs some checks
// Pathfinding itself works, but the enemy can't properly handle tile hitboxes which causes him to get stuck, should be fixed later on with collision fixes.
namespace EnterTheGuncave.Entities.Baddies
{
    public class TurretEnemy : Entity
    {
        private EnemyStats stats;
        private PistolShooter shooter;

        public TurretEnemy(Vector2 position)
        {
            this.position = position;
            this.team = dTeam.baddies;
            
            this.texture = AssetLoader.textures[dTextureKeys.enemy_turret];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
            
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            this.collider = new Hitbox(position, myWidth, myHeight);

            stats.speed = 0;
            stats.heartDamage = 1;
            stats.hitpoints = 10;
        }

        public override void update()
        {
            shoot();
        }

        // TODO
        private int reload = 100;
        private void shoot()
        {
            reload--;
            if (!(reload <= 0))
            {
                return;
            }

            reload = 100;
            int target_x = (int)EnterTheGuncave.entities[0].position.X;
            int target_y = (int)EnterTheGuncave.entities[0].position.Y;
                
            EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(new Vector2(target_x, target_y), position, new BulletStats(1, 1, 500, 5, dTeam.baddies)));
        }

        /* ------------------- DAMAGE ------------------- */

        public override void takeDamage(int dmg)
        {
            stats.hitpoints = stats.hitpoints - dmg;
            this.texture = AssetLoader.textures[dTextureKeys.player];
        }
        
    }
}