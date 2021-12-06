using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave
{
    public class Input
    {
        public static KeyboardState keyboardState;

        public static void updateKeyboardState()
        {
            keyboardState = Keyboard.GetState();
        }
    }
}