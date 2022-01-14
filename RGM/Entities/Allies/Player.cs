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

namespace RGM.Entities.Allies
{
    public class Player : Entity
    {
    
        private readonly SimpleAnimator animator;
        private bool shoot;

        private readonly float speed = 1f;
        private readonly float friction = 0.65f;
        private readonly float maxVelocity = 6;

        private const int invincibilityTimeMax = 10;
        private int invincibilityTime = 10;
        private bool gotHit = false;
        
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

            if (gotHit)
            {
                handleInvincibility();
            }

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
            adjustColliderPosition(position);
        }
        
        /* -------------- MOVEMENT --------------- */

        private void reactToInput()
        {
            if (!(velocity.Y < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.W))
                {
                    velocity.Y -= 1 * speed;
                }
            }

            if (!(velocity.Y > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.S))
                {
                    velocity.Y += 1 * speed;
                }
            }

            if (!(velocity.X < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.D))
                {
                    velocity.X += 1 * speed;
                }
            }

            if (!(velocity.X > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.A))
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

            Entity collidingBody = CollisionUtils.checkCollisionAtPos(collider, position, velocity * speed);
            
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

        private void handleInvincibility()
        {
            invincibilityTime--;

            if (invincibilityTime <= 0)
            {
                gotHit = false;
            }
        }

        public override void takeDamage(int dmg)
        {
            if (gotHit) return;
            
            gotHit = true;
            invincibilityTime = invincibilityTimeMax;
            
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
