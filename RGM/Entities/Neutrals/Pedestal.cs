using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;

namespace RGM.Entities.Neutrals
{
    public class Pedestal : Entity
    {

        public Pedestal(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures[dTextureKeys.pedestal];
            this.myWidth  = texture.Width  / RGM.scale;
            this.myHeight = texture.Height / RGM.scale;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

    }
}