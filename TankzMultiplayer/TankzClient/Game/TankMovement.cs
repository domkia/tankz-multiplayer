using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankMovement : TankPhase
    {
        float speed = 30;
        int movDir;
        Vector2 position;

        public TankMovement(Tank tank) : base (tank)
        {
            
        }

        private void MoveTank(float deltaTime)
        {
            if (movDir == 0)
            {
                return;
            }

            position = tank.transform.position;
            //TODO: apribojimai 
            tank.transform.SetPosition(position + new Vector2(movDir* deltaTime * speed, 0));


        }
        public override void Update(float deltaTime)
        {

            if (Input.IsKeyDown(System.Windows.Forms.Keys.Left))
            {
                movDir = -1;
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.Right))
            {
                movDir = 1;
            }
            else
                movDir = 0;
            MoveTank(deltaTime);
        }
    }
}
