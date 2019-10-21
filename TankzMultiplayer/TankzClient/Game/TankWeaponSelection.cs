using System;
using System.Collections.Generic;
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
