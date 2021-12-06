using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave
{
    public class Input
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;

        public static void updateKeyboardState()
        {
            keyboardState = Keyboard.GetState();
        }

        public static void updateMouseState()
        {
            mouseState = Mouse.GetState();
        }
    }
}