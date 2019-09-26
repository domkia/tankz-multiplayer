
namespace TankzClient.Framework.Components
{
    /// <summary>
    /// Component interface
    /// </summary>
    public interface IComponent
    {
        void Update(float deltaTime, Entity entity);
    }
}
