using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;

namespace RGM.General.Graphics
{
    public class textInfo
    {
        public textInfo(string text, int time, bool forever, Vector2 position, dFontKeys font, Color color)
        {
            this.text = text;
            this.position = position;
            this.font = font;
            this.time = time;
            this.forever = forever;
            this.color = color;

            if (this.forever)
            {
                time = 1;
            }

        }
        
        public string text;
        public readonly Vector2 position;
        public readonly dFontKeys font;
        public Color color;
        
        public readonly bool forever;
        public int time;

    }

    public static class FontRenderer
    {
        public static readonly List<textInfo> textQueue = new List<textInfo>();
        public static readonly List<textInfo> dissappearingTextQueue = new List<textInfo>();

        public static void renderQueue()
        {
            foreach (var info in textQueue)
            {
                renderText(info.text, info.position, info.font, info.color);

                if (!info.forever)
                {
                    info.time--;
                }

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

        private static void renderText(string text, Vector2 position, dFontKeys font, Color color)
        {
            SpriteFont spriteFont = AssetLoader.fonts[font];
            
            Vector2 textMiddlePoint = spriteFont.MeasureString(text) / 2;

            RGM.spriteBatch.DrawString(spriteFont, text, position, color, 0, textMiddlePoint,
                1.0f, SpriteEffects.None , 0.5f);
        }
        
    }
}