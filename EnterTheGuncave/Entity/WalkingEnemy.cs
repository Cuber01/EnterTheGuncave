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
        
        private int[,] map = new int[EnterTheGuncave.roomWidth, EnterTheGuncave.roomHeight];

        public WalkingEnemy(Vector2 position)
        {
            this.position = position;
            
            this.texture = AssetLoader.textures["enemy"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
        }

        private int counter = 100;
        public override void update()
        {
            map = Util.fillInProximityMap(EnterTheGuncave.entities[0].tilePosition, map);
            
            Util.prettyPrint2DArray(map);
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            //System.Threading.Thread.Sleep(100);
            counter--;

            if (counter < 0)
            {
                counter = 100;                
                position = Util.tilePositionToPixelPosition(whereToGo());

            }
            
        }
        //
        // 6 5 4 3 4 5 6 7 8 
        // 5 4 3 2 3 4 5 6 7 
        // 4 3 2 1 2 3 4 5 6 
        // 3 2 1 0 1 2 3 4 5 
        // 4 3 2 1 2 3 4 5 6 
        // 5 4 3 2 3 4 5 6 7 
        // 6 5 4 3 4 5 6 7 8 
        // 7 6 5 4 5 6 7 8 9 
        // 8 7 6 5 6 7 8 9 10 
        // 9 8 7 6 7 8 9 10 11 
        // 10 9 8 7 8 9 10 11 12 
        // 11 10 9 8 9 10 11 12 13 
        // 12 11 10 9 10 11 12 13 14 
        // 13 12 11 10 11 12 13 14 15 
        // 14 13 12 11 12 13 14 15 16 


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
            
            int minValue = Int32.MaxValue;
            
            // Brute force >:(
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
            
            if( map[tilePosition.X, tilePosition.Y - 1 ] < minValue ) 
            {
                minValue = map[tilePosition.X,     tilePosition.Y - 1    ] ;
                ( newPosition.X, newPosition.Y ) = (tilePosition.X,     tilePosition.Y - 1  );
            } 

            return newPosition;
        }
        
    }
}