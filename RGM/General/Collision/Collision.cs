using Microsoft.Xna.Framework;

namespace RGM.General.Collision
{
    public struct dTrajectoryRV
    {
        public dTrajectoryRV(dDirection relPosition, Vector2 cp)
        {
            this.relPosition = relPosition;
            this.cp = cp;
        }
        
        private dDirection relPosition;
        private Vector2 cp;
    }
    
    
    public class Collision
    {
        private static bool isCollision(float new_position_x, float new_position_y, Vector2 cp, dDirection rel_position)
        {
            if (rel_position == dDirection.none) return false;
            
            bool collision = (rel_position == dDirection.left   && new_position_x >= cp.X) ||
                             (rel_position == dDirection.up    && new_position_y <= cp.Y) ||
                             (rel_position == dDirection.right  && new_position_x <= cp.X) ||
                             (rel_position == dDirection.down && new_position_y >= cp.Y);

            return collision;
        }
        
        dTrajectoryRV catchingTrajectory(float x, float y, float sx, float sy, float ox, float oy, float ow, float oh)
        {
            // calculate line equasion
            if (sx == 0) sx = (float)0.00000000001; // add anything
            Vector2 s_line  = straightLine(x, y, x + sx, y + sy);

            Vector2 left_cp = new Vector2(ox, f(ox, s_line.X, s_line.Y));

            Vector2 top_cp = crossingPoint(s_line.X, s_line.Y, 0, oy + oh);

            Vector2 right_cp = new Vector2(ox + ow, f(ox + ow, s_line.X, s_line.Y));
                x:
            Vector2 bottom_cp = crossingPoint(s_line.X, s_line.Y, 0, oy);

            dSides sides = posRelative(x, y, ox, oy, ow, oh);

            Vector2 cp = new Vector2(0, 0);
            dDirection position = dDirection.none; // TODO check

            if (sides.left)
            {
                if (left_cp.Y >= oy && left_cp.Y <= oy + oh)
                {
                    cp = left_cp;
                    position = dDirection.left;
                }
            } else if (sides.top)
            {
                if (top_cp.X >= ox && top_cp.X <= ox + ow)
                {
                    cp = top_cp;
                    position = dDirection.up;
                }
            } else if (sides.right)
            {
                if (right_cp.Y >= oy && right_cp.Y <= oy + oh)
                {
                    cp = right_cp;
                    position = dDirection.right;
                }
            } else if (sides.bottom)
            {
                if (bottom_cp.X >= ox && bottom_cp.X <= ox + ow)
                {
                    cp = bottom_cp;
                    position = dDirection.down;
                }
            }

            //helpers
            // draw_vector(x, y, x + sx, y + sy);
            // drawStraightLine(x, s_line.a, s_line.b);
            // // Helpers crossing points
            // strokeWeight(0.1);
            // fill(color("rgb(255,255,0)"));
            // circle(left_cp.x, left_cp.y, 15);
            // fill(color("rgb(255,85,0)"));
            // circle(top_cp.x, top_cp.y, 15);
            // fill(color("rgb(255,200,0)"));
            // circle(right_cp.x, right_cp.y, 15);
            // fill(color("rgb(255,155,0)"));
            // circle(bottom_cp.x, bottom_cp.y, 0.5);
            //
            // if (found == false)
            // {
            //     noFill();
            //     stroke(0, 255, 0);
            //     circle(cp.x, cp.y, 15);
            // }

            //print(cp)

            return new dTrajectoryRV(position, cp);
        }

        Vector2 crossingPoint(float a1, float b1, float a2, float b2)
        {
            float rv_x = (b2 - b1) / (a1 - a2);

            return new Vector2(rv_x, a1 * rv_x + b1);
        }

        dSides posRelative(float x1, float y1, float x2, float y2, float w2, float h2)
        {
            bool L = x1 <= x2;
            bool R = x1 >= x2 + w2;
            bool T = y1 >= y2 + h2;
            bool B = y1 <= y2;

            //
            //
            // if (L)
            // {
            //     line(x2, y2, x2, y2 + h2);
            // }
            //
            // if (T)
            // {
            //     line(x2, y2 + h2, x2 + w2, y2 + h2);
            // }
            //
            // if (R)
            // {
            //     line(x2 + w2, y2, x2 + w2, y2 + h2);
            // }
            //
            // if (B)
            // {
            //     line(x2, y2, x2 + w2, y2);
            // }

            return new dSides(T, B, R, L);

        }

        private static Vector2 straightLine(float x1, float y1, float x2, float y2)
        {
            float a = (y2 - y1) / (x2 - x1);
            float b = -(a * x1 - y1);

            return new Vector2(a, b);
        }

        private static float f(float x, float a, float b)
        {
            var y = a * x + b;
            return y;
        }

        // function drawStraightLine(x1, a, b)
        // {
        //     stroke(0, 0, 0);
        //     strokeWeight(1);
        //     let step = 1;
        //     for (x = x1 - 50; x < x1 + 50; x += step)
        //     {
        //         circle(x, f(x, a, b), step);
        //     }
        // }
    }
}