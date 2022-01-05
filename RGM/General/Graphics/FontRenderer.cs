using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.General.ContentHandling.Assets;

namespace RGM.General.Graphics
{
    public struct textInfo
    {
        
        public textInfo(string text, Vector2 position, dFontKeys font)
        {
            this.text = text;
            this.position = position;
            this.font = font;
        }
        
        public readonly string text;
        public readonly Vector2 position;
        public readonly dFontKeys font;

    }

    public static class FontRenderer
    {
        private static List<textInfo> textQueue = new List<textInfo>();

        public static void renderQueue()
        {
            foreach (var text in textQueue)
            {
                renderText(text.text, text.position, text.font);
            }
        }
        
        private static void renderText(string text, Vector2 position, dFontKeys font)
        {
            SpriteFont spriteFont = AssetLoader.fonts[font];
            
            Vector2 textMiddlePoint = spriteFont.MeasureString(text) / 2;

            RGM.spriteBatch.DrawString(spriteFont, text, position, Color.White, 0, textMiddlePoint,
                1.0f, SpriteEffects.None , 0.5f);
        }
        
    }
}