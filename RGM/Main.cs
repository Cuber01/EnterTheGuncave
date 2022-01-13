using System;
using RGM.General.xGraphics;

namespace RGM
{

    public static class MyGlobals {
        public static CGraphicsOutput canvas= new CGraphicsOutput();
        public static double i = 0;
    }

    public static class Program
    {

        [STAThread]
        static void Main()
        {
            using var game = new RGM();

            game.Run();
        }
    }
}