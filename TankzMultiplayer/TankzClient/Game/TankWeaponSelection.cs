using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankWeaponSelection : TankPhase
    {
        InventoryUI inventoryUI;
        public TankWeaponSelection(Tank tank) : base(tank)
        {
            inventoryUI = new InventoryUI(new Rectangle (100,100,50,50));
        }
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.W))
            {
                if (inventoryUI.IsOpen) inventoryUI.ClosePanel();
                else
                {
                    inventoryUI.OpenPanel();
                }
            }
        }
    }
}
