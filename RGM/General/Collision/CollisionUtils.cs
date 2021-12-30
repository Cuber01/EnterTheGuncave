using System;
using Microsoft.Xna.Framework;
using RGM.Entities;
using RGM.Entities.Projectiles;

namespace RGM.General.Collision
{
    public static class CollisionUtils
    {
        public static Entity checkCollisionAtPos(Hitbox myHitbox, Vector2 myPosition, Vector2 myVelocity)
        {

            foreach (Entity entity in RGM.entities)
            {
                Hitbox otherHitbox = entity.collider;

                if (myHitbox != otherHitbox && !(entity is Bullet))
                {
                    
                    Vector2 newPosition = myPosition + myVelocity;

                    dTrajectoryRV a = (Collision.catchingTrajectory(
                        myPosition.X + myHitbox.width/2, myPosition.Y + myHitbox.height/2, myVelocity.X, myVelocity.Y, 
                        otherHitbox.position.X - myHitbox.width/2, otherHitbox.position.Y - myHitbox.height/2, 
                        otherHitbox.width + myHitbox.width, otherHitbox.height + myHitbox.height)
                        );
                    
                    if (a.relPosition != dDirection.none)
                    {
                        RGM.draw.drawCircle((int)a.cp.X, (int)a.cp.Y, 5, Color.Beige);    
                    }
                    
                    RGM.draw.drawRectangle(new Rectangle(
                          (int)(otherHitbox.position.X - myHitbox.width/2),
                        (int)(otherHitbox.position.Y - myHitbox.height/2),
                        otherHitbox.width + myHitbox.width,
                        otherHitbox.height + myHitbox.height
                        ),
                        Color.Aqua, false);
                    
                    
                    if(Collision.isCollision(newPosition.X, newPosition.Y, a.cp, a.relPosition))

                        // Console.Write((int)a.relPosition + (string)" ");
                    // Console.Write(a.cp + "\n");
                    
                    
                    // if (myPosition.X < otherHitbox.position.X + otherHitbox.width  &&
                    //     myPosition.X + myHitbox.width > otherHitbox.position.X     &&
                    //     myPosition.Y < otherHitbox.position.Y + otherHitbox.height &&
                    //     myHitbox.height + myPosition.Y > otherHitbox.position.Y)
                    {
                        return entity;
                    }    
                }
                      
            }
            
            return null;
            
        }
    }
}