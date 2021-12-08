using System;
using System.Collections.Generic;
using System.Linq;
using EnterTheGuncave.Content;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave
{
    public class WalkingEnemy : Entity
    {
        private float speed = 1;
        
        private readonly int[,] map = new int[EnterTheGuncave.roomWidth, EnterTheGuncave.roomHeight];

        public WalkingEnemy(Vector2 position)
        {
            this.position = position;
            
            this.texture = AssetLoader.textures["enemy"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
            map = Util.fillInProximityMap(new Point(2, 2), map);
        }

        private int counter = 100;
        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            //System.Threading.Thread.Sleep(100);
            counter--;

            if (counter < 0)
            {
                counter = 100;                
                position = Util.tilePositionToPixelPosition(whereToGo());

            }
            
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
        }

        private Point whereToGo()
        {
            if (map[tilePosition.X, tilePosition.Y] == 0)
            {
                return new Point(tilePosition.X, tilePosition.Y);
            }
            
            
            int minValue = 999;
            
            Point newPosition = new Point(-1, -1);
            if( map[tilePosition.X + 1, tilePosition.Y - 1  ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y - 1 ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X + 1, tilePosition.Y);
            } 
            
            if( map[tilePosition.X + 1, tilePosition.Y      ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X + 1, tilePosition.Y  );
            } 
            
            if( map[tilePosition.X + 1, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X + 1, tilePosition.Y+1 ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X + 1, tilePosition.Y+1);
            }
            
            if( map[tilePosition.X, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X, tilePosition.Y+1] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X,     tilePosition.Y+1  );
            }  
            
            if( map[tilePosition.X - 1, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y+1 ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X - 1, tilePosition.Y+1);
            }
            
            if( map[tilePosition.X - 1, tilePosition.Y ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y      ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X - 1, tilePosition.Y  );
            } 
            
            if( map[tilePosition.X - 1, tilePosition.Y-1 ] < minValue ) 
            {
                minValue = map[tilePosition.X - 1, tilePosition.Y-1    ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X - 1, tilePosition.Y-1);
            }
            
            if( map[tilePosition.X, tilePosition.Y+1 ] < minValue ) 
            {
                minValue = map[tilePosition.X,     tilePosition.Y+1    ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X,     tilePosition.Y+1  );
            } 
            
            Console.WriteLine(newPosition);
            return newPosition;
        }
        
    }
}