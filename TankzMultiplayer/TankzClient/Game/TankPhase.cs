namespace TankzClient.Game
{
    /// <summary>
    /// Strategy base class
    /// </summary>
    abstract class TankPhase
    {
        public abstract void Update(Tank tank, float deltaTime);
    }
}
