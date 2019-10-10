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
        private Tank tank;

        public TankAiming(Tank tank, float angle)
        {
            this.angle = angle;
            this.tank = tank;
            Input.OnKeyDown += (o, k) => {

                if (k == System.Windows.Forms.Keys.Left)
                {
                    rotDir = -1;
                }
                else if (k == System.Windows.Forms.Keys.Right)
                {
                    rotDir = 1;
                }
            };
            Input.OnKeyUp += (o, k) => rotDir = 0;
        }

        float angle = -90;
        private void RotateBarrel()
        {
            if (rotDir == 0)
            {
                return;
            }
            angle += rotDir * 5;
            if (angle < -180)
                angle = -180;
            else if (angle > 0)
                angle = 0;
            TransformComponent t = tank.barrel.GetComponent<TransformComponent>();
            t.SetAngle(angle);
        }
        int rotDir = 0;
        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }

    }
}
