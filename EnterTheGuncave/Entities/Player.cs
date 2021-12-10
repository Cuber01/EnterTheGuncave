using EnterTheGuncave.Content;
using EnterTheGuncave.Entities.Projectile;
using EnterTheGuncave.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave.Entities
{
    public class Player : Entity
    {

        private float speed = 1;
        private float friction = 0.6f;
        private float maxVelocity = 2;

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
            
            reactToInput();

            checkVelocity();
            applyFriction();
            move();
        }

        private void reactToInput()
        {
            if (Input.keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += 1 * speed;
            }
            
            if (Input.keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1 * speed;
            }

            if (Input.mouseWasClicked())
            {
                EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(Input.mouseState.X, Input.mouseState.Y, position, new BulletStats(1, 1, 500, 5)));
            }
        }

        private void applyFriction()
        {
            if (velocity.X < 0)
            {
                velocity.X += friction;
            } else if (velocity.X > 0)
            {
                velocity.X -= friction;
            }
            
            if (velocity.Y < 0)
            {
                velocity.Y += friction;
            } else if (velocity.Y > 0)
            {
                velocity.Y -= friction;
            }
        }

        private void checkVelocity()
        {
            if (velocity.X > maxVelocity)
            {
                velocity.X = maxVelocity;
            } else if (velocity.X < -maxVelocity)
            {
                velocity.X = -maxVelocity;
            }
            
            if (velocity.Y > maxVelocity)
            {
                velocity.Y = maxVelocity;
            } else if (velocity.Y < -maxVelocity)
            {
                velocity.Y = -maxVelocity;
            }
        }

        private void move()
        {
            position += velocity * speed;
        }

    }
}