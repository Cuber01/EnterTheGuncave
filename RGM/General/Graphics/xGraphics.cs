using System;
using System.Collections.Generic;
using RGM.General.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// See https://aka.ms/new-console-template for more information

namespace RGM.General.xGraphics
{


    public class CGraphObject
    {
        public string type = "None";
        protected int x = 0;
        protected int y = 0;

        protected Color c;        
        public CGraphObject()
        {

        }

        public override string ToString()
        {
            return this.type + "(" + x + "," + y + ")";
        }

        public virtual void draw(DrawUtils draw_context)
        {

        }
    }


    public class CCircle : CGraphObject
    {
        protected int r;
        public CCircle(int x, int y, int r, Color c ) : base()
        {

            this.x = x;
            this.y = y;
            this.r = r;
            this.c = c;
            this.type = "CIRCLE";
        }

        public override void draw(DrawUtils draw_context)
        {
            draw_context.drawCircle(this.x, this.y,this.r, this.c);
        }
    }

    public class CLine : CGraphObject
    {
        protected int r;
        protected int x2 = 0;
        protected int y2 = 0;

        public CLine(int x1, int y1, int x2, int y2, Color c ) : base()
        {

            this.x = x1;
            this.y = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.c = c;

            this.r = r;
            this.type = "LINE";
        }

        public override  void draw(DrawUtils draw_context)
        {
            draw_context.drawLine(this.x,this.y,this.x2,this.y2,c);
        }
    }

    public class CRectangle : CGraphObject
    {
        protected int r;
        protected int w;
        protected int h;

        public CRectangle(int x1, int y1, int w, int h, Color c ) : base()
        {

            this.x = x1;
            this.y = y1;
            this.w = w;
            this.h = h;
            this.c = c;

            this.r = r;
            this.type = "RECTANGLE";
        }

        public override  void draw(DrawUtils draw_context)
        {
            draw_context.drawRectangle( new Rectangle(this.x,this.y,this.w,this.h ),this.c, false ) ;
        }
    }


    public class CGraphicsOutput
    {

        protected Queue<CGraphObject> ObjectsQueue;
        public CGraphicsOutput()
        {
            ObjectsQueue = new Queue<CGraphObject>();
        }

        public void circle(int x, int y, int r, Color c ) 
        {
            ObjectsQueue.Enqueue(new CCircle(x, y, r, c));
        }

        public void line(int x1, int y1, int x2, int y2, Color c )
        {
            ObjectsQueue.Enqueue(new CLine(x1, y1, x2, y2, c));
        }
        public void rect(int x1, int y1, int x2, int y2, Color c )
        {
            ObjectsQueue.Enqueue(new CRectangle(x1, y1, x2, y2, c));
        }


        public void draw(DrawUtils draw_context)
        {

            while (ObjectsQueue.Count != 0)
            {
                ObjectsQueue.Dequeue().draw(draw_context);
                // Console.WriteLine("{0}", o );

            }

        }

    }
}
// class Test00
// {
//     static void Main()
//     {

//         CGraphicsOutput canvas = new CGraphicsOutput();
//         canvas.circle(10,11,12);
//         canvas.line(10,11,12,14);
//         canvas.circle(10,11,12);
//         canvas.draw();

//     }
// }