using System.Drawing;

namespace TankzClient.Game
{
    /// <summary>
    /// Base tank decorator class which extends rendering behaviour
    /// </summary>
    abstract class TankDecorator : TankChassis
    {
        public TankChassis tank;

        public TankDecorator(TankChassis tank) 
            : base(tank.image, tank.transform.position, tank.transform.size)
        {
            this.tank = tank;
            this.parent = tank.parent;
            this.transform = tank.transform;
        }

        public override void Render(Graphics context)
        {
            tank.Render(context);
        }

        public override void Update(float deltaTime)
        {
            tank.Update(deltaTime);
        }
    }
}
