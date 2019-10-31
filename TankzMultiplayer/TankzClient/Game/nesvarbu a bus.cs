using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Game
{
    class nesvarbu_a_bus
    {
        void DoMovement()
        { 
            Susiejimas b = new ClientTankMovement();
            Susiejimas adapter = new Adapter(new ServerTankMovement());

            b.Move();
            adapter.Move();
        }


    }
    public class ServerTankMovement
    {
        public void NetworkMove()
        {

        }
    }

    public class Adapter : Susiejimas
    {
        ServerTankMovement serverTank;
        public Adapter(ServerTankMovement serverTank)
        {
            this.serverTank = serverTank;
        }
        public void Move()
        {
            serverTank.NetworkMove();
        }
    }
    public class ClientTankMovement : Susiejimas
    {
        public void Move()
        {

        }
    }

    interface Susiejimas
    {
        void Move();
    }
}
