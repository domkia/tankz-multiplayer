using TankzClient.Framework;

namespace TankzClient.Game
{
    /// <summary>
    /// Tank movement command. When you undo this command
    /// the tank goes back to its initial position.
    /// Fuel is also compensated.
    /// </summary>
    class TankMoveCommand : ITankCommand
    {
        private readonly int startFuel;
        private readonly Vector2 startPosition;
        private readonly ITank tank;

        public TankMoveCommand(ITank tank)
        {
            this.tank = tank;
            this.startFuel = tank.Fuel;
            this.startPosition = tank.Position;
        }

        public void Execute(float distance)
        {
            Vector2 offset = Vector2.right * distance;
            this.tank.Move(offset);
        }

        public void Undo()
        {
            this.tank.SetFuel(startFuel);
            Tank tankEntity = tank as Tank;
            tankEntity.transform.SetPosition(startPosition);
        }
    }
}
