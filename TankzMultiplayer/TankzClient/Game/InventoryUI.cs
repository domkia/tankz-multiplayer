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
        public override void Render(Graphics context)
        {
            context.DrawRectangle(Pens.Gray, rect);
        }
        private bool isOpen = false;
        public bool IsOpen => isOpen;
        public InventoryUI(Rectangle rect) : base(rect)
        {
            
        }
        public override void Click(Point mousePos)
        {
            //nieko nera
        }

        public void OpenPanel()
        {
            isOpen = true;
        }

        public void ClosePanel()
        {
            isOpen = false;
        }
    }
}
