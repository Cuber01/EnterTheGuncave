using System;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Projectiles
{
    public class Bullet : Entity
    {
        private readonly Vector2 target;
        private BulletStats stats;

        public Bullet(Vector2 targetPos, Vector2 position, BulletStats stats)
        {
            this.position = position;
            this.texture = AssetLoader.textures[dTextureKeys.bullet];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.target = targetPos;
            this.stats = stats;
            
            this.collider = new Hitbox(position, myWidth, myHeight);
            goToPoint();
        }

        public override void update()
        {
            handleLifetime();
            
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

        private void handleLifetime()
        {
            stats.lifetime--;

            if (stats.lifetime <= 0)
            {
                dead = true;
            }
        }
        
        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (collider.checkCollision(entity) == null) continue;
                
                if (entity.team == stats.team || entity is Bullet)
                {
                    return;
                }

                entity.takeDamage(stats.damage);

                stats.penetration--;
                dead = true;
            }
        }
        
        private void move()
        {
            position += velocity * stats.speed;
        }

    }
}