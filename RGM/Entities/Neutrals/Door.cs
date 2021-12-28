using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.ContentHandling.Rooms;
using RGM.General.DungeonGenerator;
using RGM.General.EventHandling;

// Static imports. ono.
using static RGM.RGM;

namespace RGM.Entities.Neutrals
{
    public class Door : Entity
    {
        private readonly dDirection direction;
        private bool isOpen = false;

        private static readonly Rectangle[] spritesheetPositions =
        {
            new Rectangle(0, 0, 16, 16),
            new Rectangle(0, 16, 16, 16),
            new Rectangle(16, 16, 16, 16),
            new Rectangle(16, 0, 16, 16),
            
            new Rectangle(32, 0, 16, 16) // Closed.
        };

        public Door(Vector2 position, dDirection direction)
        {
            this.position = position;
            this.direction = direction;
            this.texture  = AssetLoader.textures[dTextureKeys.arrow];
            
            // Determining width using tileSize instead of texture width as usual
            this.myWidth  = RGM.tileSize / RGM.scale; 
            this.myHeight = RGM.tileSize / RGM.scale;
            
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);
            
            GEventHandler.subscribe(open, dEvents.roomClear);
        }

        private void open(dEvents e)
        {
            isOpen = true;
        }

        public override void draw()
        {
            RGM.spriteBatch.Draw(texture, position,
                isOpen ? spritesheetPositions[(int)direction] : 
                         spritesheetPositions[4], 
                Color.White);
        }

        public override void onPlayerCollision()
        {
            // If the door is closed, don't go anywhere.
            if (!isOpen)
            {
                return;
            }
            
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
            
            RoomLoader.playRoom(DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomIndex, DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomType, direction);
            RoomLoader.changingRoom = true;
        }
    }
}