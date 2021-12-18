using EnterTheGuncave.Entities;
using EnterTheGuncave.Entities.Projectiles;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.Collision
{
    public static class CollisionUtils
    {
        public static bool checkCollisionAtPos(Hitbox myHitbox, Vector2 myPosition)
        {

            foreach (Entity entity in EnterTheGuncave.entities)
            {
                Hitbox otherHitbox = entity.collider;

                if (myHitbox != otherHitbox && !(entity is Bullet))
                {
                    if (myPosition.X < otherHitbox.position.X + otherHitbox.width  &&
                        myPosition.X + myHitbox.width > otherHitbox.position.X     &&
                        myPosition.Y < otherHitbox.position.Y + otherHitbox.height &&
                        myHitbox.height + myPosition.Y > otherHitbox.position.Y)
                    {
                        return true;
                    }    
                }
                      
            }
            
            return false;
            
        }
    }
}