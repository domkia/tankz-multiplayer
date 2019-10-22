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
        private bool isOpen = false;
        public bool IsOpen => isOpen;
        public InventoryUI(Rectangle rect) : base(rect)
        {
            
        }
        public override void Click(Point mousePos)
        {
            throw new NotImplementedException();
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
