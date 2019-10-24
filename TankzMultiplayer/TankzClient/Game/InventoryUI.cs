using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class InventoryUI : UIElement
    {
        private Pen borderPen = new Pen(Color.DarkRed, 4);
        private Pen markedBorderPen = new Pen(Color.Bisque, 4);
        private int weaponElementSize = 120;
        private int padding = 15;
        private int selectedWeapon = 1;

        public override void Render(Graphics context)
        {
            context.FillRectangle(Brushes.Brown, rect);
            context.DrawRectangle(borderPen, rect);
            context.FillRectangle(Brushes.Brown, rect.X + padding, rect.Y + padding, weaponElementSize, weaponElementSize);
            context.FillRectangle(Brushes.Brown, rect.X + padding * 2 + weaponElementSize, rect.Y + padding, weaponElementSize, weaponElementSize);
            context.DrawRectangle(borderPen, rect.X + padding * 2 + weaponElementSize, rect.Y + padding, weaponElementSize, weaponElementSize);
            context.FillRectangle(Brushes.Brown, rect.X + padding, rect.Y + padding * 2 + weaponElementSize, weaponElementSize, weaponElementSize);
            context.DrawRectangle(borderPen, rect.X + padding, rect.Y + padding * 2 + weaponElementSize, weaponElementSize, weaponElementSize);
            context.FillRectangle(Brushes.Brown, rect.X + padding * 2 + weaponElementSize, rect.Y + padding * 2 + weaponElementSize, weaponElementSize, weaponElementSize);
            context.DrawRectangle(borderPen, rect.X + padding * 2 + weaponElementSize, rect.Y + padding * 2 + weaponElementSize, weaponElementSize, weaponElementSize);
            
            context.DrawRectangle(markedBorderPen, rect.X + padding, rect.Y + padding, weaponElementSize, weaponElementSize);
        }

        public InventoryUI(Rectangle rect) : base(rect)
        {

        }
        public override void Click(Point mousePos)
        {
            //nieko nera
        }
    }
}
