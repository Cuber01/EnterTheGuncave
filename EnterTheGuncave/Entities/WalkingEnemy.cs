using System;
using EnterTheGuncave.Content;
using EnterTheGuncave.General.Collision;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.Entities
{
    public class WalkingEnemy : Entity
    {
        private float speed = 1;
        
        private int[,] map = new int[EnterTheGuncave.roomWidth, EnterTheGuncave.roomHeight];

        public WalkingEnemy(Vector2 position)
        {
            this.position = position;
            
            this.texture = AssetLoader.textures["enemy"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
            
            this.collider = new Hitbox(position, myWidth, myHeight);
        }


        public override void update()
        {
            map = Util.fillInProximityMap(EnterTheGuncave.entities[0].tilePosition, map);
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            adjustColliderPosition();
            checkCollision();

            goToPoint(EnterTheGuncave.entities[0].position);
            move();
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
            Vector2 newPosition = position + velocity * speed;

            if (CollisionUtils.checkCollisionAtPos(collider, newPosition))
            {
                return;
            }
            
            position = newPosition;
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
        
        /* ---------------- COLLISION ------------------ */
        
        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (collider.checkCollision(entity) != null)
                {
                    Console.WriteLine("ASAAAAAAAAAAAAAAAA");
                }
            }
        }
        
    }
}