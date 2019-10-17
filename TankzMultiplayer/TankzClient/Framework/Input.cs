using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TankzClient.Framework
{
    static class Input
    {
        public static List<Keys> inputQueue = new List<Keys>();

        public static event EventHandler<MouseArgs> OnMouseClick;

        public static bool IsKeyDown(Keys key) => inputQueue.Contains(key);

        public static bool IsKeyUp(Keys key) => !IsKeyDown(key);

        public static bool MouseButtonDown => mouseDown;

        private static bool mouseDown = false;
        private static Point mousePosition = new Point();
        public static Point MousePosition => mousePosition;

        internal static void HandleMouseClick(MouseEventArgs args)
        {
            OnMouseClick?.Invoke(null, new MouseArgs(args));
            mouseDown = true;
        }

        internal static void Reset()
        {
            mouseDown = false;
            inputQueue.Clear();
        }

        internal static void HandleKeyDown(Keys key)
        {
            if (!inputQueue.Contains(key))
                inputQueue.Add(key);
        }

        internal static void HandleKeyUp(Keys key)
        {
            if(inputQueue.Contains(key))
                inputQueue.Remove(key);
        }

        internal static void HandleMousePosition(int x, int y)
        {
            mousePosition = new Point(x, y);
        }
    }

    public class MouseArgs : EventArgs
    {
        public readonly MouseButtons button;
        public Vector2 mousePosition;

        public MouseArgs(MouseEventArgs args)
        {
            this.button = args.Button;
            this.mousePosition = new Vector2((float)args.Location.X, (float)args.Location.Y);
        }
    }
}
