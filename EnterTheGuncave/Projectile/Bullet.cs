using EnterTheGuncave.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave.Projectile
{
    public class Bullet
    {
        private Texture2D texture;

        private int myWidth;
        private int myHeight;

        private Vector2 target;
        private int speed;
        private int penetration;
        private int lifetime;
        private int damage;

        private Vector2 position;
        private Vector2 velocity;

        public Bullet(Vector2 target, int speed = 1, int penetration = 1, int lifetime = 500, int damage = 5)
        {
            this.texture = AssetLoader.textures["bullet"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.target = target;
            this.speed = speed;
            this.penetration = penetration;
            this.lifetime = lifetime;
            this.damage = damage;
            
            goToPoint(target);
        }

        
        public void update()
        {
            move();
        }

        public void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
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