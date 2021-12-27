using Microsoft.Xna.Framework;
using RGM.Entities;
using RGM.Entities.Projectiles;

namespace RGM.General.Collision
{
    public static class CollisionUtils
    {
        public static Entity checkCollisionAtPos(Hitbox myHitbox, Vector2 myPosition)
        {

            foreach (Entity entity in RGM.entities)
            {
                Hitbox otherHitbox = entity.collider;

                if (myHitbox != otherHitbox && !(entity is Bullet))
                {
                    if (myPosition.X < otherHitbox.position.X + otherHitbox.width  &&
                        myPosition.X + myHitbox.width > otherHitbox.position.X     &&
                        myPosition.Y < otherHitbox.position.Y + otherHitbox.height &&
                        myHitbox.height + myPosition.Y > otherHitbox.position.Y)
                    {
                        return entity;
                    }    
                }
                      
            }
            
            return null;
            
        }
    }
}