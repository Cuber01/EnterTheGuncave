using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Projectiles
{
    public class Bullet : Entity
    {
        private Vector2 target;
        private BulletStats stats;

        public Bullet(int targetX, int targetY, Vector2 position, BulletStats stats)
        {
            this.position = position;
            this.texture = AssetLoader.textures["bullet"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            ( this.target.X, this.target.Y ) = ( targetX, targetY );
            this.stats = stats;
            
            this.collider = new Hitbox(position, myWidth, myHeight);
            goToPoint();
        }

        public override void update()
        {
            move();
            
            checkCollision();
            adjustColliderPosition();
        }

        private void goToPoint()
        {
            float dist = Util.calculateDistance(position, target);
            this.velocity.X = (target.X - position.X) / dist;
            this.velocity.Y = (target.Y - position.Y) / dist;
        }
        
        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (collider.checkCollision(entity) != null)
                {
                    if (entity.team == stats.team)
                    {
                        return;
                    }

                    entity.takeDamage(stats.damage);
                    dead = true;

                }
            }
        }
        
        private void move()
        {
            position += velocity * stats.speed;
        }

    }
}