using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;

using static RGM.RGM;

namespace RGM.Entities.Neutrals
{
    public class Stone : Entity
    {

        private readonly int textureNmb;

        // List of possible, randomly chosen, stone texture variations
        private static readonly Rectangle[] spritesheetPositions =
        {
            new Rectangle(0,             0, tileSize, tileSize),
            new Rectangle(tileSize,      0, tileSize, tileSize),
            new Rectangle(tileSize * 2,  0, tileSize, tileSize),
        };
        
        public Stone(Vector2 position)
        {
            this.position = position;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            this.texture  = AssetLoader.textures[dTextureKeys.stone];
            this.textureNmb = Util.random.Next(0, 2);
            
            this.myWidth  = RGM.tileSize;
            this.myHeight = RGM.tileSize;

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void draw()
        {
            RGM.spriteBatch.Draw(texture, position, spritesheetPositions[textureNmb], Color.White);
        }
        
        ~Stone()
        {
            RGM.currentRoom[tilePosition.X, tilePosition.Y] = 0;
        }

    }
}