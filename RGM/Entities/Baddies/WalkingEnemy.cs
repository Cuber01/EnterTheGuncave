using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RGM.Entities.Allies;
using RGM.Entities.Projectiles;
using RGM.General.Animation;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.EventHandling;

// TODO pathfinding needs some checks
// Pathfinding itself works, but the enemy can't properly handle tile hitboxes which causes him to get stuck, should be fixed later on with collision fixes.
namespace RGM.Entities.Baddies
{
    public class WalkingEnemy : Entity
    {
        private readonly SimpleAnimator animator;
        private new EntityStats stats;
        
        private int[,] map = new int[RGM.roomWidth, RGM.roomHeight];

        public WalkingEnemy(Vector2 position)
        {
            this.position = position;
            this.team = dTeam.baddies;

            this.texture = AssetLoader.textures[dTextureKeys.enemy];
            this.myWidth  = texture.Width;
            this.myHeight = texture.Height;
            
            this.collider = new Hitbox(position, 9, 7);
            this.animator = new SimpleAnimator(texture, animation);

            stats = new EntityStats(
                0.2f,
                1,
                5,
                0,
                0,
                0,
                0,
                0
                );
            
        }


        public override void update()
        {
            // Cut pathfinding :(
            
            // map = Util.fillInProximityMap(RGM.Player.tilePosition, map);
            // tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            // goToTile(whereToGo());

            goToPoint(RGM.Player.position);
            move();
            
            adjustColliderPosition(new Vector2(position.X+1, position.Y+2));
        }
        
        /* ---------------- MOVEMENT ------------------ */
        
        private void goToPoint(Vector2 target)
        {
            float dist = Util.calculateDistance(position, target);
            this.velocity.X = (target.X - position.X) / dist;
            this.velocity.Y = (target.Y - position.Y) / dist;
        }
        
        private void goToTile(Point target)
        {
            Vector2 pixelTarget = Util.tilePositionToPixelPosition(target);
            
            float dist = Util.calculateDistance(position, pixelTarget);
            this.velocity.X = (pixelTarget.X - position.X) / dist;
            this.velocity.Y = (pixelTarget.Y - position.Y) / dist;
        }

        private void move()
        {
            Vector2 newPosition = position + velocity * stats.speed;


            if(CollisionUtils.checkCollisionAtPos(collider, newPosition, velocity) is Player || CollisionUtils.checkCollisionAtPos(collider, newPosition, velocity) is null)
            {
                position = newPosition;
            }

        }

        public override void draw()
        {
            animator.draw(position);
        }

        private Point whereToGo()
        {
            if (map[tilePosition.X, tilePosition.Y] == 0)
            {
                return new Point(tilePosition.X, tilePosition.Y);
            }
            
            int minValue = Int32.MaxValue;
            
            // Brute force >:(
            // It's fast though...
            Point newPosition = new Point(-1, -1);
            if( map[tilePosition.X + 1, tilePosition.Y - 1  ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y - 1 ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X + 1, tilePosition.Y);
            } 
            
            if( map[tilePosition.X + 1, tilePosition.Y      ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X + 1, tilePosition.Y  );
            } 
            
            if( map[tilePosition.X + 1, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y+1 ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X + 1, tilePosition.Y+1);
            }
            
            if( map[tilePosition.X, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X, tilePosition.Y+1] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X,     tilePosition.Y+1  );
            }  
            
            if( map[tilePosition.X - 1, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y+1 ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X - 1, tilePosition.Y+1);
            }
            
            if( map[tilePosition.X - 1, tilePosition.Y ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y      ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X - 1, tilePosition.Y  );
            } 
            
            if( map[tilePosition.X - 1, tilePosition.Y-1 ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y-1    ] ;
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X - 1, tilePosition.Y-1);
            }
            
            if( map[tilePosition.X, tilePosition.Y - 1 ] < minValue ) 
            {
                ( newPosition.X, newPosition.Y ) = ( tilePosition.X,     tilePosition.Y - 1 );
            } 

            return newPosition;
        }

        /* ------------------- DAMAGE ------------------- */

        public override void onPlayerCollision()
        {
            // Ugly hacks say hello.
            RGM.entities[0].takeDamage(stats.damage);
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
                GEventHandler.fireEvent(dEvents.enemyKilled);
                dead = true;
                
                return;
            }
            
            GEventHandler.fireEvent(dEvents.enemyHurt);
        }
        
        /* --------------------- ANIMATION ------------------- */
        
        private static readonly Dictionary<Rectangle, int> animation = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 0, 11, 10), 200
            },
           
            {
                new Rectangle(11, 0, 11, 10), 200
            },
            
            {
                new Rectangle(22, 0, 11, 10), 200
            },

            {
                new Rectangle(33, 0, 11, 10), 50
            }

        };
        
    }
}