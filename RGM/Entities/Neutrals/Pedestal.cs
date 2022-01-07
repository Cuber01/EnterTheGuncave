using System;
using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.Items;

namespace RGM.Entities.Neutrals
{
    public class Pedestal : Entity
    {
        public BaseItem item;
        public int itemIndex = 0;
        
        private bool hasItem = true; 
        
        private readonly Vector2 itemPosition;
        
        public Pedestal(Vector2 position, int itemIndex)
        {
            this.position = position;
            this.texture  = AssetLoader.textures[dTextureKeys.pedestal];
            this.myWidth  = texture.Width;
            this.myHeight = texture.Height;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.itemIndex = itemIndex;

            if (itemIndex != 9999)
            {
                this.item = Activator.CreateInstance(RGM.allItems[itemIndex]) as BaseItem;
            }
            else
            {
                hasItem = false;
            }
            
            
            this.itemPosition = new Vector2(position.X, position.Y - 5);
            
            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public void setItem(int newItemIndex)
        {
            if (newItemIndex == 9999)
            {
                this.item = null;
                hasItem = false;
                
                return;
            }

            this.item = Activator.CreateInstance(RGM.allItems[newItemIndex]) as BaseItem;
            hasItem = true;
        }

        public override void onPlayerCollision()
        {
            
            if (hasItem)
            {
                item.pickedUp();
                RGM.Player.inventory.Add(item);
                item = null;
                itemIndex = 9999;
                
                hasItem = false;
            }
            
        }

        public override void draw()
        {
            base.draw();

            if (hasItem)
            {
                // Draw item
                RGM.spriteBatch.Draw(item.texture, itemPosition, Color.White);
            }
            
        }
    }
}