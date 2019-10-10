using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankzClient.Framework
{
    static class Input
    {
        public static event EventHandler<MouseArgs> OnMouseClick;
        public static event EventHandler<Keys> OnKeyDown;
        public static event EventHandler<Keys> OnKeyUp;

        static internal void HandleMouseClick(MouseEventArgs args) => OnMouseClick?.Invoke(null, new MouseArgs(args));
        static internal void HandleKeyDown(KeyEventArgs args) => OnKeyDown?.Invoke(null, args.KeyCode);
        static internal void HandleKeyUp(KeyEventArgs args) => OnKeyUp?.Invoke(null, args.KeyCode);
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
