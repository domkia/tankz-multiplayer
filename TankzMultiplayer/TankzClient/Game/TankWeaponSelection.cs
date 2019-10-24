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
        private InventoryUI inventoryUI;
        public TankWeaponSelection(Tank tank) : base(tank)
        {
            inventoryUI = new InventoryUI(new Rectangle(250, 150, 285, 285));
            SceneManager.Instance.CurrentScene.CreateEntity(inventoryUI);
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.W))
            {
                SceneManager.Instance.CurrentScene.DestroyEntity(inventoryUI);
                tank.SetPhase(new TankAiming(tank));
            }
        }
    }
}
