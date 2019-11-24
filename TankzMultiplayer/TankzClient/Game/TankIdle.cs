using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Game
{
    class TankIdle : TankPhase
    {
        public TankIdle(PlayerTank tank) : base(tank)
        {
            Console.WriteLine("STATE new TankIdle() Phase");
        }

        public override void Update(float deltaTime)
        {
            
        }
    }
}
