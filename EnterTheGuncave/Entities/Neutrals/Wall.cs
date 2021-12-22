using System.Collections.Generic;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using EnterTheGuncave.General.ContentHandling.Rooms;
using Microsoft.Xna.Framework;
using static EnterTheGuncave.EnterTheGuncave;

namespace EnterTheGuncave.Entities.Neutrals
{
    public class Wall : Entity
    {
        private dTileDirection direction;
        
        // TODO this will initialize only once, right?
        private static Rectangle[] spritesheetPositions =
        {
            // walls
            new Rectangle(tileSize, tileSize * 2, tileSize, tileSize),                  // up
            new Rectangle(tileSize, 0, tileSize, tileSize),                             // down
            new Rectangle(0, tileSize, tileSize, tileSize),                             // right
            new Rectangle(tileSize * 2, tileSize, tileSize, tileSize),                  // et cetera, see tileDirection.cs for details 
            
            // corners
            new Rectangle(tileSize * 2, 0, tileSize, tileSize),
            new Rectangle(0, 0, tileSize, tileSize),
            new Rectangle(tileSize * 2, tileSize * 2, tileSize, tileSize),
            new Rectangle(0, tileSize * 2, tileSize, tileSize),
        };

        public Wall(Vector2 position, dTileDirection direction)
        {
            this.position = position;
            this.direction = direction;
            this.texture  = AssetLoader.textures[dTextureKeys.tiles1];
            
            this.myWidth  = EnterTheGuncave.tileSize * EnterTheGuncave.scale; // Determining width using tileSize instead of texture width as usual
            this.myHeight = EnterTheGuncave.tileSize * EnterTheGuncave.scale;
            
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, spritesheetPositions[(int)direction], Color.White);
        }
    }
}