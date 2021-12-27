using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.Items;

namespace RGM.Entities
{
    public class Entity
    {
        public Vector2 position;
        public Point tilePosition;
        public Point mapPosition;

        public dTeam team;
        public bool dead;

        public Vector2 velocity;
        protected Texture2D texture;
        public Hitbox collider;

        public int myWidth;
        protected int myHeight;

        /* -------------------- MAIN -------------------- */

        public virtual void update() { }

        public virtual void draw()
        {
            RGM.spriteBatch.Draw(texture, position, Color.White);
        }

        /* ------------------ COLLISION ------------------ */

        protected virtual void checkCollision() { }

        public virtual void onPlayerCollision() { }

        protected void adjustColliderPosition()
        {
            collider.position = this.position;
        }

        /* ------------------- DAMAGE ------------------- */

        public virtual void takeDamage(int dmg) { }
     
        /* ------------------- PLAYER ------------------- */
        
        public List<BaseItem> inventory = new List<BaseItem>();

        // TODO tmp
        public int health;

    }
}