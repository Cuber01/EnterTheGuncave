using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RGM.General.Animation
{
    public class Animator
    {
        private int timeToNextFrame;
        private int currentFrame;
        
        private readonly Texture2D spritesheet;
        private readonly Dictionary<Rectangle, int> frames;

        public Animator(Texture2D spritesheet, Dictionary<Rectangle, int> frames)
        {
            this.spritesheet = spritesheet;
            this.frames = frames;
        }

        public void draw(Vector2 position)
        {
            if (timeToNextFrame <= 0)
            {
                currentFrame++;

                if (currentFrame > frames.Count - 1)
                {
                    currentFrame = 0;
                }
                
                timeToNextFrame = frames.ElementAt(currentFrame).Value;
            }

            timeToNextFrame--;
            
            RGM.spriteBatch.Draw(spritesheet, position, frames.ElementAt(currentFrame).Key, Color.White);
        }
    }
}