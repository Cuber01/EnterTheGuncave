using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General;
using EnterTheGuncave.General.Collision;
using EnterTheGuncave.General.ContentHandling.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave.Entities.Allies
{
    public class Player : Entity
    {

        private readonly float speed = 1.4f;
        private readonly float friction = 0.65f;
        private readonly float maxVelocity = 6;

        public Player(Vector2 position)
        {
            this.position = position;
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
                int target_x = Input.mouseState.X / EnterTheGuncave.scale;
                int target_y = Input.mouseState.Y / EnterTheGuncave.scale;
                
                EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(target_x, target_y, position, new BulletStats(1, 1, 500, 5, dTeam.allies)));
            }
        }

        private void applyFriction()
        {
            velocity.X = velocity.X * friction;
            velocity.Y = velocity.Y * friction;
        }

        private void move()
        {
            Vector2 newPosition = position + velocity * speed;
            
                
            if (CollisionUtils.checkCollisionAtPos(collider, newPosition))
            {
                    // int xo = 0;
                    // int dx; int dy;
                    // dx = (velocity.X < 0) ? -1: 1;
                    // dy = (velocity.Y < 0) ? -1: 1;                    
                    //
                    // for (int x = (int)position.X; x <= newPosition.X; x++)// to musi by petla while z dx
                    // {
                    //     if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(x, newPosition.Y)))
                    //     {
                    //         // position = new Vector2(x , newPosition.Y);                            
                    //         xo = x;
                    //         break;
                    //     }
                    //
                    // }
                    //
                    // for (int y = (int)position.X; y <= newPosition.X; y++)// to musi by petla while z dy
                    // {
                    //     if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(newPosition.X, y)))
                    //     {
                    //         // position = new Vector2(x , newPosition.Y);                            
                    //         position = new Vector2(xo , y);
                    //         break;
                    //     }
                    //
                    // }
                    //

                    (velocity.X, velocity.Y) = (0, 0);
                    return;
            }
            else
            {
                position = newPosition;    
            }
            
        }
        
        /* ---------------- COLLISION ------------------ */

        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (collider.checkCollision(entity) != null)
                {
                    //    velocity = -velocity;
                }
            }
        }




    }
}







        // move for faster movement prototype
        // private void move()
        // {
        //     Vector2 newPosition = position + velocity * speed;
        //     
        //     if (CollisionUtils.checkCollisionAtPos(collider, newPosition))
        //     {
        //         if (position.X < newPosition.X && (int)position.Y == (int)newPosition.Y)
        //         {
        //             
        //             for (int x = (int)position.X; x <= newPosition.X; x++)
        //             {
        //                 if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(x, newPosition.Y)))
        //                 {
        //                     position = new Vector2(x - 1, newPosition.Y);
        //                     return;
        //                 }
        //             }
        //             
        //         }
        //         else if (position.X > newPosition.X && (int)position.Y == (int)newPosition.Y)
        //         {
        //             
        //             for (int x = (int)position.X; x >= newPosition.X; x--)
        //             {
        //                 if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(x, newPosition.Y)))
        //                 {
        //                     position = new Vector2(x + 1, newPosition.Y);
        //                     return;
        //                 }
        //             }
        //             
        //         }
        //         else if ((int)position.X == (int)newPosition.X && (int)position.Y < (int)newPosition.Y)
        //         {
        //             
        //             for (int y = (int)position.Y; y <= (int)newPosition.Y; y++)
        //             {
        //                 if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(y, newPosition.Y)))
        //                 {
        //                     position = new Vector2(newPosition.X, y - 1);
        //                     return;
        //                 }
        //             }
        //             
        //         }
        //         else if ((int)position.X == (int)newPosition.X && position.Y > newPosition.Y)
        //         {
        //             
        //             for (int y = (int)position.Y; y >= newPosition.Y; y--)
        //             {
        //                 if (CollisionUtils.checkCollisionAtPos(collider, new Vector2(y, newPosition.Y)))
        //                 {
        //                     position = new Vector2(newPosition.X, y + 1);
        //                     return;
        //                 }
        //             }
        //             
        //         }
        //     }
        //
        //     position = newPosition;
        // }