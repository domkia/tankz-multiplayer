
namespace TankzClient.Game
{
    /// <summary>
    /// Tank command interface
    /// </summary>
    public interface ITankCommand
    {
        void Execute(float value);
        void Undo();
    }
}
