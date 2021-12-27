using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RGM.Entities.Neutrals;
using RGM.Entities.Projectiles;
using RGM.General;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.DungeonGenerator;
using RGM.Items;

// TODO fix penetration idot
namespace RGM.Entities.Allies
{
    public class Player : Entity
    {
        private readonly PistolShooter shooter;
        private bool shoot;

        private readonly ShooterStats shooterStats = new ShooterStats(
            10,
            1, 
            new BulletStats(
                1,
                2,
                500,
                1,
                dTeam.allies
            ));
        
        
        private readonly float speed = 1.1f;
        private readonly float friction = 0.65f;
        private readonly float maxVelocity = 6;

        public Player(Vector2 position)
        {
            health = 6;

            this.position = position;
            this.mapPosition = DungeonGenerator.startingPos;
            
            this.texture  = AssetLoader.textures[dTextureKeys.player];
            this.myWidth  = texture.Width  / RGM.scale;
            this.myHeight = texture.Height / RGM.scale;

            this.shooter = new PistolShooter(shooterStats);
            
            this.team = dTeam.allies;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            reactToInput();

            if (shoot)
            {
                int targetX = Input.mouseState.X / RGM.scale;
                int targetY = Input.mouseState.Y / RGM.scale;
            
                shooter.update(position, new Vector2(targetX, targetY), true);
                shoot = false;
            }
            else
            {
                shooter.update(position, new Vector2(0, 0), false);
            }

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
                shoot = true;
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
                
                collidingBody.onPlayerCollision();
            }
            else
            {
                position = newPosition;    
            }
            
        }
        
        /* ---------------- COLLISION ------------------ */

        // protected override void checkCollision()
        // {
        //     foreach (Entity entity in RGM.entities)
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
