using System;

namespace RGM
{
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