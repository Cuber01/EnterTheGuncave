using System;
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

        protected Vector2   velocity;
        protected Texture2D texture;
        public    Hitbox    collider;

        protected int myWidth;
        protected int myHeight;
        
        // Team determines stuff like ignoring bullet collision in the same team and more.
        public dTeam team;
        
        // If this is set to true, the entity will be removed.
        public bool dead;
        
        // If an entity with this set to true is alive, the doors shall be closed.
        public bool isDangerous;

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
        
        /* ------------------- DOOR ---------------------- */

        public virtual void closeDoor()
        {
            throw new Exception("You're not quite supposed to call closeDoor on a non Door, you see?");
        }

    }
}