using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave
{
    public class Entity
    {
        public Vector2 position;
        public Texture2D texture;

        public virtual void update() { }
        
        public virtual void draw() { }
    }
}