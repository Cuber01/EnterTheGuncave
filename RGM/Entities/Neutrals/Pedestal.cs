using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.Items;

namespace RGM.Entities.Neutrals
{
    public class Pedestal : Entity
    {
        private BaseItem item;
        private bool hasItem = true; 
        
        private readonly Vector2 itemPosition;
        
        public Pedestal(Vector2 position, BaseItem item)
        {
            this.position = position;
            this.texture  = AssetLoader.textures[dTextureKeys.pedestal];
            this.myWidth  = texture.Width  / RGM.scale;
            this.myHeight = texture.Height / RGM.scale;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.item = item;
            this.itemPosition = new Vector2(position.X, position.Y - 5);
            
            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void onPlayerCollision()
        {
            
            if (hasItem)
            {
                item.pickedUp();
                RGM.Player.inventory.Add(item);
                item = null;
                
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