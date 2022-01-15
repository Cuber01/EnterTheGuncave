using System;
using Microsoft.Xna.Framework;
using RGM.Entities;
using RGM.Entities.Projectiles;
using System.Collections.Generic;
using RGM;

namespace RGM.General.Collision
{
    public static class CollisionUtils
    {
        public static (Entity, Vector2, dDirection) checkCollisionAtPos(Hitbox myHitbox, Vector2 myPosition, Vector2 myVelocity, float dt)
        {


            Vector2 newPosition = myPosition + myVelocity * dt;
            List<(Entity, Vector2, dDirection)> colisions= new List<(Entity, Vector2, dDirection)>();

            foreach (Entity entity in RGM.entities)
            {
                Hitbox otherHitbox = entity.collider;

                if (myHitbox != otherHitbox && !(entity is Bullet))
                {

                    //---               
                    dTrajectoryRV a = (Collision.catchingTrajectory(
                        myPosition.X + myHitbox.width / 2, myPosition.Y + myHitbox.height / 2, myVelocity.X, myVelocity.Y,
                        otherHitbox.position.X - myHitbox.width / 2, otherHitbox.position.Y - myHitbox.height / 2,
                        otherHitbox.width + myHitbox.width, otherHitbox.height + myHitbox.height)
                        );

                    if (myVelocity.X == 0 && myVelocity.Y == 0)
                    {

                    }
                    else

                    if (a.relPosition != dDirection.none)
                    {
                        // RGM.draw.drawCircle((int)a.cp.X, (int)a.cp.Y, 5, Color.Beige);    
                        MyGlobals.canvas.circle((int)a.cp.X, (int)a.cp.Y, 4, Color.Beige);
                        // MyGlobals.canvas.rect (50,50,30,30, Color.Beige);
                    }

                    // MyGlobals.canvas.rect(
                    //     (int)(otherHitbox.position.X - myHitbox.width/2),
                    //     (int)(otherHitbox.position.Y - myHitbox.height/2),
                    //     otherHitbox.width + myHitbox.width,
                    //     otherHitbox.height + myHitbox.height
                    //     ,
                    //     Color.Aqua);

                    int x0 = (int)(myPosition.X + myHitbox.width / 2);
                    int y0 = (int)(myPosition.Y + myHitbox.height / 2);
                    int r0 = 10;
                    MyGlobals.canvas.line(x0, y0, (int)(x0 + r0 * Math.Cos(MyGlobals.i)), (int)(y0 + r0 * Math.Sin(MyGlobals.i)), Color.Coral);
                    MyGlobals.i += 0.01;


                    // RGM.draw.drawRectangle(new Rectangle(
                    //       (int)(otherHitbox.position.X - myHitbox.width/2),
                    //     (int)(otherHitbox.position.Y - myHitbox.height/2),
                    //     otherHitbox.width + myHitbox.width,
                    //     otherHitbox.height + myHitbox.height
                    //     ),
                    //     Color.Aqua, false);


                    // //if(Collision.isCollision(newPosition.X, newPosition.Y, a.cp, a.relPosition))

                    // string[] d= { "up" , "down" ,"right" , "left" ,  "center", "none" };
                    // Console.Write(d[(int)a.relPosition] + (string)"\n");

                    // // Console.Write(a.cp + "\n");
                    // if (a.cp.X == 0 && a.cp.Y == 0)
                    // {
                    //     return (null, new Vector2(0, 0));            
                    // }

                    if (a.relPosition != dDirection.none)
                    {
                        // if (newPosition.X < otherHitbox.position.X + otherHitbox.width &&
                        //     newPosition.X + myHitbox.width > otherHitbox.position.X &&
                        //     newPosition.Y < otherHitbox.position.Y + otherHitbox.height + 1 &&
                        //     myHitbox.height + newPosition.Y > otherHitbox.position.Y)

                    bool colision = false;
                    switch( a.relPosition ){
                        case dDirection.left:
                            colision = ( newPosition.X + myHitbox.width/2 - 1  >= (int)a.cp.X );
                        if(colision){                            
                            newPosition.X = a.cp.X - myHitbox.width / 2 - 1 ;
                            newPosition.Y = a.cp.Y - myHitbox.height / 2 ;
                        }

                        break;
                        case dDirection.up:
                            colision = ( newPosition.Y + myHitbox.height/2 -1 <= (int)a.cp.Y);
                        if(colision){                            
                            newPosition.X = a.cp.X - myHitbox.width / 2  ;
                            newPosition.Y = a.cp.Y - myHitbox.height / 2 + 1;
                        }
                        break;
                        case dDirection.right:
                            colision = ( newPosition.X + myHitbox.width/2 -1 <= (int)a.cp.X);
                        if(colision){                            
                            newPosition.X = a.cp.X - myHitbox.width / 2 + 2 ;
                            newPosition.Y = a.cp.Y - myHitbox.height / 2 ;
                        }
                        break;
                        case dDirection.down:
                        
                            colision = ( newPosition.Y + myHitbox.height/2 +1 >= (int)a.cp.Y );
                                                    if(colision){
                            newPosition.X = a.cp.X - myHitbox.width / 2  ;
                            newPosition.Y = a.cp.Y - myHitbox.height / 2 - 1 ;
                                                    }

                        break;
                    }            

                        if(colision){
                            //return (entity, new Vector2(a.cp.X, a.cp.Y));
                            // newPosition.X = a.cp.X - myHitbox.width / 2 - 1 ;
                            // newPosition.Y = a.cp.Y - myHitbox.height / 2 +1 ;

                            //return (entity, newPosition, a.relPosition);
                            colisions.Add((entity, newPosition, a.relPosition) );
                            // return entity;
                        }
                    }

                }

            }

        float calcDistance(Vector2 point1, Vector2 point2)
        {
            float dx = Math.Abs(point2.X - point1.X);
            float dy = Math.Abs(point2.Y - point1.Y);
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);
            return dist;
        }


            //return (null, new Vector2(0, 0));
            if( colisions.Count > 0 ){

                if(colisions.Count>1){
                    if(colisions.Count>2){
                        throw new Exception("no to juz przesada" );
                    }
                    float min_distance = 1000;
                    int index=0;
                    int i = 0;
                     Entity e;
                       Vector2 pos;
                       dDirection d;

                    foreach( (Entity, Vector2, dDirection)c in  colisions){
                         (e,pos,d) = c;
                         
                        float distance = calcDistance( myPosition, e.collider.position );
                        if( min_distance > distance ){
                            min_distance = distance;
                            index = i;                            
                        }
                        i++;
                    }
                    return colisions[index];
                }
                

                return colisions[0];
            }

            return (null, newPosition, dDirection.none);
            // return null;

        }
    }



}