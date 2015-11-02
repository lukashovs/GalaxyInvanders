using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GalaxyInvanders.Screens.Menu
{
    internal static class InputManager
    {
        static KeyboardState keyState;
        static KeyboardState keyOldState;
        static MouseState mouseState;
        static MouseState mouseOldState;

        static InputManager()
        {
        }

        internal static void Update(GameTime gameTime)
        {
            keyOldState = keyState;
            mouseOldState = mouseState;

            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        internal static bool IsKeyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        internal static bool IsKeyPress(Keys key)
        {
            return IsKeyDown(key) && keyOldState.IsKeyUp(key);
        }

        internal static bool IsMouseLeftDown()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        internal static bool IsMouseLeftClick()
        {
            return IsMouseLeftDown() && mouseOldState.LeftButton == ButtonState.Released;
        }

        internal static bool IsMouseRightDown()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        internal static bool IsMouseRightClick()
        {
            return IsMouseRightDown() && mouseOldState.RightButton == ButtonState.Released;
        }

        internal static bool IsMouseMiddleDown()
        {
            return mouseState.MiddleButton == ButtonState.Pressed;
        }

        internal static bool IsMouseMiddleClick()
        {
            return IsMouseMiddleDown() && mouseOldState.MiddleButton == ButtonState.Released;
        }

        internal static bool IsMouseWheelUp()
        {
            return mouseState.ScrollWheelValue > mouseOldState.ScrollWheelValue;
        }

        internal static bool IsMouseWheelDown()
        {
            return mouseState.ScrollWheelValue < mouseOldState.ScrollWheelValue;
        }

        internal static Vector2 GetMousePositionToVector2()
        {
            return new Vector2(mouseState.X, mouseState.Y);
        }

        internal static Point GetMousePositionToPoint()
        {
            return new Point(mouseState.X, mouseState.Y);
        }

    }
}
