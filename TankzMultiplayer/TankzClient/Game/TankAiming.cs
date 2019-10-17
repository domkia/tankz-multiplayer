using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Framework.Components;

namespace TankzClient.Game
{
    class TankAiming : TankPhase
    {
        int rotDir = 0;

        public TankAiming(Tank tank) : base (tank)
        {
            this.tank = tank;
        }

        private void RotateBarrel(float deltaTime)
        {
            if (rotDir == 0)
            {
                return;
            }
            
            tank.barrel.angle += rotDir * 5;
            if (tank.barrel.angle < -180)
                tank.barrel.angle = -180;
            else if (tank.barrel.angle > 0)
                tank.barrel.angle = 0;
            TransformComponent t = tank.barrel.GetComponent<TransformComponent>();
            t.SetAngle(tank.barrel.angle);
        }
        
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Left))
            {
                rotDir = -1;
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.Right))
            {
                rotDir = 1;
            }
            else rotDir = 0;
            RotateBarrel(deltaTime);
        }

    }
}
