using EnterTheGuncave.Content;
using EnterTheGuncave.Entities.Projectiles;
using EnterTheGuncave.General;
using EnterTheGuncave.General.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave.Entities.Allies
{
    public class Player : Entity
    {

        private float speed = 1.1f;
        private float friction = 0.65f;
        private float maxVelocity = 6;

        public Player(Vector2 position)
        {
            this.position = position;
            this.texture  = AssetLoader.textures["player"];
            this.myWidth  = texture.Width  / EnterTheGuncave.scale;
            this.myHeight = texture.Height / EnterTheGuncave.scale;

            this.team = dTeam.allies;
            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            reactToInput();
            
            applyFriction();
            move();
            
            checkCollision();
            adjustColliderPosition();
        }
        
        /* -------------- MOVEMENT --------------- */

        private void reactToInput()
        {
            if (!(velocity.Y < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= 1 * speed;
                }
            }

            if (!(velocity.Y > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Down))
                {
                    velocity.Y += 1 * speed;
                }
            }

            if (!(velocity.X < -maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Right))
                {
                    velocity.X += 1 * speed;
                }
            }

            if (!(velocity.X > maxVelocity))
            {
                if (Input.keyboardState.IsKeyDown(Keys.Left))
                {
                    velocity.X -= 1 * speed;
                }
            }

            if (Input.mouseWasClicked())
            {
                int target_x = Input.mouseState.X / EnterTheGuncave.scale;
                int target_y = Input.mouseState.Y / EnterTheGuncave.scale;
                
                EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(target_x, target_y, position, new BulletStats(1, 1, 500, 5, dTeam.allies)));
            }
        }

        private void applyFriction()
        {
            velocity.X = velocity.X * friction;
            velocity.Y = velocity.Y * friction;
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
        
        /* ---------------- COLLISION ------------------ */

        protected override void checkCollision()
        {
            foreach (Entity entity in EnterTheGuncave.entities)
            {
                if (collider.checkCollision(entity) != null)
                {
                //    velocity = -velocity;
                }
            }
        }




    }
}