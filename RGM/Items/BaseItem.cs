using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;
using RGM.General.Graphics;

namespace RGM.Items
{
    public class BaseItem
    {
        protected string name;
        protected string description;

        public Texture2D texture;
        private dItems item;

        protected BaseItem(dItems item, Texture2D texture)
        {
            this.item = item;
            this.texture = texture;
        }

        public virtual void activate(dEvents e = dEvents.none) { }

        public virtual void pickedUp()
        {
            FontRenderer.textQueue.Add(new textInfo(name,        200, false, new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 300), dFontKeys.pico8_big, Color.White));
            FontRenderer.textQueue.Add(new textInfo(description, 200, false, new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 250), dFontKeys.pico8_small, Color.White));
        }
    }
    
}