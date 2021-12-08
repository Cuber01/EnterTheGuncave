using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave
{
    public class Entity
    {
        protected Vector2 position;
        protected Point tilePosition;
        protected Texture2D texture;

        protected int myWidth;
        protected int myHeight;

        public virtual void update() { }
        
        public virtual void draw() { }
    }
}