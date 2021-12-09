using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave.Projectile
{
    public class Bullet
    {
        private int speed;
        private int penetration;
        private int lifetime;
        private int damage;

        private Vector2 position;
        private Vector2 velocity;

        public Bullet(int speed = 1, int penetration = 1, int lifetime = 500, int damage = 5)
        {
            this.speed = speed;
            this.penetration = penetration;
            this.lifetime = lifetime;
            this.damage = damage;
        }

        
        public void update()
        {
            
        }

        public void draw()
        {
            
        }
        
        private void goToPoint(Vector2 target)
        {
            float dist = Util.calculateDistance(position, target);
            this.velocity.X = (target.X - position.X) / dist;
            this.velocity.Y = (target.Y - position.Y) / dist;
        }
        
        private void move()
        {
            position += velocity * speed;
        }

    }
}