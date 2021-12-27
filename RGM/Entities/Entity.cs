using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.Entities.Projectiles;
using RGM.General.Collision;

namespace RGM.Entities
{
    public class Entity
    {
        public Vector2 position;
        public Point   tilePosition;
        public Point   mapPosition;
        
        public dTeam team;
        public bool dead;

        public    Vector2   velocity;
        protected Texture2D texture;
        public    Hitbox    collider;

        public int    myWidth;
        protected int myHeight;

        /* -------------------- MAIN -------------------- */
        
        public virtual void update() { }

        public virtual void draw()
        {
            RGM.spriteBatch.Draw(texture, position, Color.White);
        }
        
        /* ------------------ COLLISION ------------------ */

        protected virtual void checkCollision() {}

        protected void adjustColliderPosition()
        {
            collider.position = this.position;
        }
        
        /* ------------------- DAMAGE ------------------- */

        public virtual void takeDamage(int dmg) {}
        
        /* ------------------- DOOR --------------------- */
        public virtual void playerGoThrough() {} 
        
    }
}