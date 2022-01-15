using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RGM.Entities.Projectiles;
using RGM.General.Animation;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.DungeonGenerator;
using RGM.General.EventHandling;
using RGM.General.Input;

using Microsoft.Xna.Framework.Graphics;

namespace RGM.Entities.Allies
{
    public class Player : Entity
    {
    
        private readonly SimpleAnimator animator;
        private bool shoot;

        // (
        // 10,
        // 1, 
        // new BulletStats(
        //     1,
        //     2,
        //     500,
        //     1,
        // dTeam.allies
        // ));

        
        private readonly float speed = 1f;
        private readonly float friction = 0.65f;
        private readonly float maxVelocity = 1;

        public Player(Vector2 position)
        {
            // Position
            this.position = position;
            this.mapPosition = DungeonGenerator.startingPos;
            
            // Drawing
            this.texture  = AssetLoader.textures[dTextureKeys.player];
            this.myWidth  = 6;
            this.myHeight = 7;
            
            this.animator = new SimpleAnimator(texture, animation);
            
            // Collision
            this.team = dTeam.allies;
            this.collider = new Hitbox(position, myWidth, myHeight);
            
            // Stats
            stats.speed = 1;
            stats.damage = 1;
            stats.hitpoints = 5;

            stats.penetration = 1;
            stats.bulletSpeed = 1;
            stats.range = 100;
            stats.reloadTime = 25;
            stats.spread = 1;
            
            this.shooterStats = new ShooterStats(
                stats.reloadTime,
                stats.spread, 
            
                new BulletStats(
                    stats.bulletSpeed,
                    stats.penetration,
                    stats.range,
                    stats.damage,
                    team
                )  
            );
            
            this.shooter = new Pistol(shooterStats, dTextureKeys.player_bullet);

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

            // applyFriction();

            // checkCollision();            
            move();
            
            adjustColliderPosition();
        }
        
        /* -------------- MOVEMENT --------------- */

        private bool x_lock = false;

        private void reactToInput()
        {

                if (Input.keyboardState.IsKeyDown(Keys.W))
                {
                    velocity.Y = -maxVelocity;
                }else if (Input.keyboardState.IsKeyDown(Keys.S)){

                    velocity.Y = velocity.Y = maxVelocity;
                }else{

                    velocity.Y = 0;
                }

                // if( !this.x_lock ){
                if(true){
                if (Input.keyboardState.IsKeyDown(Keys.D))
                {
                    velocity.X = maxVelocity;
                }else if (Input.keyboardState.IsKeyDown(Keys.A))
                {

                    velocity.X = -maxVelocity;;
                }else{

                    velocity.X = 0;
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
            // Console.WriteLine(velocity.X +" " +velocity.Y);


            // Vector2 newPosition = position + velocity * speed;
            
            // (Entity, Vector2) collidingBody = CollisionUtils.checkCollisionAtPos(collider, position, velocity * speed);
            //     
            // if (collidingBody.Item1 != null)
            // {
            //     (velocity.X, velocity.Y) = (0, 0);
            //
            //     collidingBody.Item1.onPlayerCollision();
            //     position = new Vector2(collidingBody.Item2.X + myWidth/2, collidingBody.Item2.Y + myHeight/2);    
            // }
            // else
            // {
            //     position = newPosition;    
            // }
            
            ( Entity collidingBody, Vector2 newPosition, dDirection relPosition ) = CollisionUtils.checkCollisionAtPos(collider, position, velocity, speed );
            
            if (collidingBody != null)
            {
                position = newPosition;    

                Console.WriteLine(new Random().NextDouble()+"colision\n");

                // (velocity.X, velocity.Y) = (0, 0);

                    switch( relPosition ){
                        case dDirection.left:
                           velocity.X = 0; 
                           this.x_lock = true;
                        break;
                        case dDirection.up:
                            velocity.Y=0;
                        break;
                        case dDirection.right:
                            velocity.X = 0;
                            this.x_lock = true;
                        break;
                        case dDirection.down:
                            velocity.Y=0;
                        break;
                    }
            
                collidingBody.onPlayerCollision();
            }
            else
            {
                position = newPosition;    
            }
                
            // if (collidingBody.Item1 != null)
            // {
            //     (velocity.X, velocity.Y) = (0, 0);
            //
            //     collidingBody.Item1.onPlayerCollision();
            //     position = new Vector2(collidingBody.Item2.X + myWidth/2, collidingBody.Item2.Y + myHeight/2);    
            // }
            // else
            // {
            //     position = newPosition;    
            // }
            
        }

        public override void takeDamage(int dmg)
        {
            stats.hitpoints = stats.hitpoints - dmg;
            checkDeath();
        }

        private void checkDeath()
        {
            
            if(stats.hitpoints <= 0)
            {
                GEventHandler.fireEvent(dEvents.playerKilled);
                dead = true;
                
                return;
            }
            
            GEventHandler.fireEvent(dEvents.playerHurt);
            
        }

    

        public override void draw()
        {
            animator.draw(position);
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
        
        /* ----------------- ANIMATION ----------------- */
        
        private static readonly Dictionary<Rectangle, int> animation = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 0, 6, 7), 50
            },
           
            {
                new Rectangle(6, 0, 6, 7), 50
            }
            
        };



        
    }
}
