using Microsoft.Xna.Framework.Input;

// Input should be handled with out event handle so we can rebing buttons instead of hardcoding them. The thing is, I don't want to rebind buttons.
namespace RGM.General.Input
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