using EnterTheGuncave.Entities.Neutrals;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using EnterTheGuncave.General.DungeonGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave.Entities.Allies
{
    public class Player : Entity
    {
        private readonly float speed = 1.1f;
        private readonly float friction = 0.65f;
        private readonly float maxVelocity = 6;

        public Player(Vector2 position)
        {
            this.position = position;
            this.mapPosition = DungeonGenerator.startingPos;
            
            this.texture  = AssetLoader.textures[dTextureKeys.player];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.team = dTeam.allies;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            reactToInput();
            
            applyFriction();
            move();
            
            checkCollision();
            adjustColliderPosition();
        }
        
        /* -------------- MOVEMENT --------------- */

        private void reactToInput()
        {
            if (!(velocity.Y < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= 1 * speed;
                }
            }

            if (!(velocity.Y > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Down))
                {
                    velocity.Y += 1 * speed;
                }
            }

            if (!(velocity.X < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Right))
                {
                    velocity.X += 1 * speed;
                }
            }

            if (!(velocity.X > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Left))
                {
                    velocity.X -= 1 * speed;
                }
            }

            if (Input.mouseWasClicked())
            {
                int targetX = Input.mouseState.X / EnterTheGuncave.scale;
                int targetY = Input.mouseState.Y / EnterTheGuncave.scale;
                
                EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(new Vector2(targetX, targetY),position, new BulletStats(1, 1, 500, 5, dTeam.allies)));
            }
        }

        private void applyFriction()
        {
            velocity.X *= friction;
            velocity.Y *= friction;
        }


        private void move()
        {
            Vector2 newPosition = position + velocity * speed;

            Entity collidingBody = CollisionUtils.checkCollisionAtPos(collider, newPosition);
                
            if (collidingBody != null)
            {
                (velocity.X, velocity.Y) = (0, 0);

                if (collidingBody is Door)
                {
                    collidingBody.playerGoThrough();
                }
            }
            else
            {
                position = newPosition;    
            }
            
        }
        
        /* ---------------- COLLISION ------------------ */

        // protected override void checkCollision()
        // {
        //     foreach (Entity entity in EnterTheGuncave.entities)
        //     {
        //         
        //         if (entity is Door)
        //         {
        //             if (collider.checkCollision(entity) != null)
        //             {
        //             
        //             }    
        //         }
        //         
        //     }
        // }

        
    }
}
