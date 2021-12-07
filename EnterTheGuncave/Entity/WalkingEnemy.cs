using System;
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
            map = Util.fillInProximityMap(new Point(3, 3), map);
        }

        public override void update()
        {
              
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}