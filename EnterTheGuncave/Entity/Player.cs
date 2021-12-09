using EnterTheGuncave.Content;
using EnterTheGuncave.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave
{
    public class Player : Entity
    {

        private float speed = 1;

        public Player(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures["player"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            if (Input.keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= 1 * speed;
            }

            if (Input.keyboardState.IsKeyDown(Keys.Space))
            {
                position = Util.tilePositionToPixelPosition(tilePosition);
            }
            //
            // Console.WriteLine(position);
            // Console.WriteLine(tilePosition);
        }

        public override void draw()
        {
            EnterTheGuncave.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}