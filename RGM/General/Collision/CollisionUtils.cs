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

                    if (newPosition.X < otherHitbox.position.X + otherHitbox.width  &&
                        newPosition.X + myHitbox.width > otherHitbox.position.X     &&
                        newPosition.Y < otherHitbox.position.Y + otherHitbox.height &&
                        myHitbox.height + newPosition.Y > otherHitbox.position.Y)
                    {
                        //return (entity, new Vector2(a.cp.X, a.cp.Y));
                        return entity;
                    }    
                    
                }
                      
            }
            
            //return (null, new Vector2(0, 0));
            return null;

        }
    }
}