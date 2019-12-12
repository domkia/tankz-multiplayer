using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Game
{
    class TankDead : TankPhase
    {
        public TankDead(PlayerTank tank) : base(tank)
        {
            Console.WriteLine("STATE new TankDead() Phase");
        }

        public override void Update(float deltaTime)
        {

        }
    }
}
