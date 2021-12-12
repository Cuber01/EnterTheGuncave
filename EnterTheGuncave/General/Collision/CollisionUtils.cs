using EnterTheGuncave.Entities;
using Microsoft.Xna.Framework;

namespace EnterTheGuncave.General.Collision
{
    public class CollisionUtils
    {
        public static bool checkCollisionAtPos(Hitbox myHitbox, Vector2 myPosition)
        {

            foreach (Entity entity in EnterTheGuncave.entities)
            {
                Hitbox otherHitbox = entity.collider;

                if (myHitbox != otherHitbox)
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