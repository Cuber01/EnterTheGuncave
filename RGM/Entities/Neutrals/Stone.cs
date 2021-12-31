using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;

namespace RGM.Entities.Neutrals
{
    public class Stone : Entity
    {

        public Stone(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures[dTextureKeys.stone];
            this.myWidth  = 16;
            this.myHeight = 16; //TODO
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        ~Stone()
        {
            RGM.currentRoom[tilePosition.X, tilePosition.Y] = 0;
        }

    }
}