using System;
using EnterTheGuncave.General.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave.Entities
{
    public class Entity
    {
        public    Vector2 position;
        public    Point   tilePosition;

        protected Vector2   velocity;
        protected Texture2D texture;
        public    Hitbox    collider;

        protected int myWidth;
        protected int myHeight;

        public virtual void update() { }

        public void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
        }

        protected virtual void checkCollision() { }

        protected void adjustColliderPosition()
        {
            collider.position = this.position;
        }
        
    }
}