using TankzClient.Framework;

namespace TankzClient.Game
{
    public interface IMediator
    {
        void Notify(Entity sender, string action);
    }
}
