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
            map = Util.fillInProximityMap(new Point(3, 3), map);
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
        }

        private Point whereToGo()
        {
            Dictionary<int, Point> surroundingTiles = getSurroundingTiles();
         
            int[] tileValues = surroundingTiles.Keys.ToArray();
            Point[] tilePositions = surroundingTiles.Values.ToArray();
            
            int smallest = Array.IndexOf(tileValues, tileValues.Min());

            return tilePositions[smallest];
        }

        private Dictionary<int, Point> getSurroundingTiles()
        {
            Dictionary<int, Point> surroundingTiles = new Dictionary<int, Point>
            {
                { map[tilePosition.X + 1, tilePosition.Y - 1  ], new Point(tilePosition.X + 1, tilePosition.Y - 1) },
                { map[tilePosition.X + 1, tilePosition.Y      ], new Point(tilePosition.X + 1, tilePosition.Y      ) },
                { map[tilePosition.X + 1, tilePosition.Y+1    ], new Point(tilePosition.X + 1, tilePosition.Y+1  ) },
                { map[tilePosition.X,     tilePosition.Y+1    ], new Point(tilePosition.X,      tilePosition.Y+1    ) },
                { map[tilePosition.X - 1, tilePosition.Y+1    ], new Point(tilePosition.X - 1, tilePosition.Y+1  ) },
                { map[tilePosition.X - 1, tilePosition.Y      ], new Point(tilePosition.X - 1, tilePosition.Y      ) },
                { map[tilePosition.X - 1, tilePosition.Y-1    ], new Point(tilePosition.X - 1, tilePosition.Y-1  ) },
                { map[tilePosition.X,     tilePosition.Y+1    ], new Point(tilePosition.X,     tilePosition.Y+1    ) }
            };

            return surroundingTiles;
        }
    }
}