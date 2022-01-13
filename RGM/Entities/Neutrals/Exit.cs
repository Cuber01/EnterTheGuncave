using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;

namespace RGM.Entities.Neutrals
{
    public class Exit : Entity
    {
        
        public Exit(Vector2 position)
        {
            // Set direction and position
            this.position = position;

            // Get texture
            this.texture = AssetLoader.textures[dTextureKeys.exit];

            // Calculate tile position
            this.myWidth = RGM.tileSize;
            this.myHeight = RGM.tileSize;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            // Setup collider and colliding teams
            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);

        }

        public override void draw()
        {
            RGM.spriteBatch.Draw(texture, position, Color.White);
        }

        
        public override void onPlayerCollision()
        {
            RGM.gameState = dGameState.won;
        }

    }
}