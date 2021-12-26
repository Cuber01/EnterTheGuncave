using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Neutrals
{
    public class Pedestal : Entity
    {

        public Pedestal(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures[dTextureKeys.pedestal];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

    }
}