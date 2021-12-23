using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using EnterTheGuncave.General.ContentHandling.Rooms;
using EnterTheGuncave.General.DungeonGenerator;
using Microsoft.Xna.Framework;

// Static imports. ono.
using static EnterTheGuncave.EnterTheGuncave;

namespace EnterTheGuncave.Entities.Neutrals
{
    public class Door : Entity
    {
        private readonly dDirection direction;
        
        private static readonly Rectangle[] spritesheetPositions =
        {
            new Rectangle(0, 0, 16, 16),
            new Rectangle(0, 16, 16, 16),
            new Rectangle(16, 16, 16, 16),
            new Rectangle(16, 0, 16, 16)
        };

        public Door(Vector2 position, dDirection direction)
        {
            this.position = position;
            this.direction = direction;
            this.texture  = AssetLoader.textures[dTextureKeys.arrow];
            
            // Determining width using tileSize instead of texture width as usual
            this.myWidth  = EnterTheGuncave.tileSize / EnterTheGuncave.scale; 
            this.myHeight = EnterTheGuncave.tileSize / EnterTheGuncave.scale;
            
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, spritesheetPositions[(int)direction], Color.White);
        }

        public override void playerGoThrough()
        {
            Entity player = entities[0];
            
            switch (direction)
            {
                case dDirection.up: 
                    player.mapPosition.Y -= 1; 
                    break;
                        
                case dDirection.down:
                    player.mapPosition.Y += 1;
                    break;
                        
                case dDirection.right:
                    player.mapPosition.X += 1;
                    break;
                        
                case dDirection.left:
                    player.mapPosition.X -= 1;
                    break;
            }    
                    
            RoomLoader.playRoom(DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomIndex);
        }
    }
}