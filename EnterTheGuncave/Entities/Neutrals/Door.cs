using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using EnterTheGuncave.General.ContentHandling.Rooms;
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
        
        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (!(entity is Player)) continue;
                
                if (collider.checkCollision(entity) != null)
                {
                    switch (direction)
                    {
                        case dDirection.up: 
                            entity.mapPosition.Y -= 1; 
                            break;
                        
                        case dDirection.down:
                            entity.mapPosition.Y += 1;
                            break;
                        
                        case dDirection.right:
                            entity.mapPosition.X += 1;
                            break;
                        
                        case dDirection.left:
                            entity.mapPosition.X -= 1;
                            break;
                    }    
                }

            }
        }
    }
}