using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace RGM.Items
{
    
    public class BaseItem
    {
        public string name;
        public string description;

        private Texture2D texture;
        private dItems item;
        
        public BaseItem(dItems item, Texture2D texture)
        {
            this.item = item;
            this.texture = texture;
        }

        public virtual void activate() { }
    }
    
}