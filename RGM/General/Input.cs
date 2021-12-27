using Microsoft.Xna.Framework.Input;

namespace RGM.General
{
    public static class Input
    {
        public static KeyboardState keyboardState;
        
        public static MouseState mouseState;
        private static MouseState oldMouseState;

        public static void updateKeyboardState()
        {
            keyboardState = Keyboard.GetState();
        }
        
        public static void updateMouseState()
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
        }
        
        public static bool mouseWasClicked()
        {
            if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }

            return false;
        }

    }
}