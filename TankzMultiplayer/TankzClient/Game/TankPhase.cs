namespace TankzClient.Game
{
    /// <summary>
    /// Strategy base class
    /// </summary>
    abstract class TankPhase
    {
        protected Tank tank;

        public TankPhase(Tank tank)
        {
            this.tank = tank;   
        }
        public abstract void Update(float deltaTime);

    }
}
