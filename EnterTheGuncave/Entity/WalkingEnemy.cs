using Microsoft.Xna.Framework;

namespace EnterTheGuncave
{
    public class WalkingEnemy : Entity
    {
        private float speed = 1;

        public WalkingEnemy(Vector2 position)
        {
            this.position = position;
            this.texture = AssetLoader.textures["enemy"];
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