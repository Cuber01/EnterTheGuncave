using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities.Neutrals
{
    public class Stone : Entity
    {

        public Stone(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures["stone"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

    }
}