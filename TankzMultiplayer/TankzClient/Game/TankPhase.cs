namespace TankzClient.Game
{
    /// <summary>
    /// Strategy base class
    /// </summary>
    public abstract class TankPhase
    {
        protected PlayerTank tank;

        public TankPhase(PlayerTank tank)
        {
            this.tank = tank;   
        }

        public abstract void Update(float deltaTime);
    }
}
