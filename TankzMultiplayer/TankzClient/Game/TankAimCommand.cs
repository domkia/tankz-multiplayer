using System;

namespace TankzClient.Game
{
    /// <summary>
    /// Tank Aiming Command. When you undo this command,
    /// the angle of the barrel resets to its initial angle
    /// </summary>
    class TankAimCommand : ITankCommand
    {
        private readonly float startAngle;
        private readonly ITank tank;

        public TankAimCommand(ITank tank)
        {
            this.tank = tank;
            this.startAngle = tank.Angle;
        }

        public void Execute(float angle)
        {
            this.tank.SetAngle(angle);
        }

        public void Undo()
        {
            this.tank.SetAngle(startAngle);
        }
    }
}
