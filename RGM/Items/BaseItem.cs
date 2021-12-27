using Microsoft.Xna.Framework.Graphics;

namespace RGM.Items
{
    
    public class BaseItem
    {
        private Texture2D texture;
        private dItems type;
        
        public BaseItem(dItems type, Texture2D texture)
        {
            this.type = type;
            this.texture = texture;
        }
    }
    
}