using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.Collision
{
    public class Hitbox
    {
        private readonly Point position;
        private readonly int   width;
        private readonly int   height;


        public Hitbox(Point position, int width, int height)
        {
            this.position = position;
            this.width = width;
            this.height = height;

        }

        public bool checkCollision(Hitbox otherHitbox)
        {
            if (position.X < otherHitbox.position.X + otherHitbox.width  &&
                position.X + width > otherHitbox.position.X              &&
                position.Y < otherHitbox.position.Y + otherHitbox.height &&
                height + position.Y > otherHitbox.position.Y)
            {
                return true;
            } 

            return false;
            
        }

        public void draw(DrawUtils draw)
        {
            draw.drawRectangle(new Rectangle(position.X, position.Y, width, height), Color.Red,false);
        }
    }
}