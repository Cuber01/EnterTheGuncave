using System;
using EnterTheGuncave.Content;
using EnterTheGuncave.Entities.Projectile;
using EnterTheGuncave.General;
using EnterTheGuncave.General.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave.Entities
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

            this.collider = new Hitbox(position, myWidth, myHeight);
        }

        public override void update()
        {
            tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            reactToInput();

            adjustColliderPosition();
            checkCollision();
            
            applyFriction();
            move();
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
                EnterTheGuncave.entitiesToBeSpawned.Add(new Bullet(Input.mouseState.X, Input.mouseState.Y, position, new BulletStats(1, 1, 500, 5)));
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
                    velocity = -velocity;
                }
            }
        }




    }
}