using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankAiming : TankPhase
    {
        int rotDir = 0;
        bool isKeyDown = false;
        public TankAiming(Tank tank) : base (tank)
        {

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
            Transform t = tank.barrel.transform;
            t.SetAngle(tank.barrel.angle);
        }
        //TODO: Fix input to be smooth without spagetti
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Left) || Input.IsKeyDown(System.Windows.Forms.Keys.Right))
            {
                isKeyDown = true;
                if (Input.IsKeyDown(System.Windows.Forms.Keys.Left))
                {
                    rotDir = -1;
                }
                else if (Input.IsKeyDown(System.Windows.Forms.Keys.Right))
                {
                    rotDir = 1;
                }
            }
            else if (Input.IsKeyUp(System.Windows.Forms.Keys.Left) || Input.IsKeyUp(System.Windows.Forms.Keys.Right))
            {
                rotDir = 0;
                isKeyDown = false;
            }

            RotateBarrel(deltaTime);
        }

    }
}
