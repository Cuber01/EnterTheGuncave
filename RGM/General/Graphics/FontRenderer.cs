using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;

namespace RGM.General.Graphics
{
    public class textInfo
    {
        public textInfo(string text, int time, Vector2 position, dFontKeys font)
        {
            this.text = text;
            this.position = position;
            this.font = font;
            this.time = time;
        }
        
        public readonly string text;
        public readonly Vector2 position;
        public readonly dFontKeys font;
        public int time;

    }

    public static class FontRenderer
    {
        public static List<textInfo> textQueue = new List<textInfo>();
        private static List<textInfo> dissappearingTextQueue = new List<textInfo>();

        public static void renderQueue()
        {
            foreach (var info in textQueue)
            {
                renderText(info.text, info.position, info.font);
                info.time--;
                
                if (info.time <= 0)
                {
                    dissappearingTextQueue.Add(info);
                }
            }

            foreach (var info in dissappearingTextQueue)
            {
                textQueue.Remove(info);
            }
            dissappearingTextQueue.Clear();
 
        }

        public static void renderText(string text, Vector2 position, dFontKeys font)
        {
            SpriteFont spriteFont = AssetLoader.fonts[font];
            
            Vector2 textMiddlePoint = spriteFont.MeasureString(text) / 2;

            RGM.spriteBatch.DrawString(spriteFont, text, position, Color.White, 0, textMiddlePoint,
                1.0f, SpriteEffects.None , 0.5f);
        }
        
    }
}