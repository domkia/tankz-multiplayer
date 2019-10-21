namespace TankzClient.Framework
{
    /// <summary>
    /// Base factory class
    /// </summary>
    public abstract class EntityFactory
    {
        public abstract Entity Create(EntityCreateArgs args);
    }

    public class EntityCreateArgs
    {
        public string type { get; }

        public EntityCreateArgs(string type)
        {
            this.type = type;
        }

        public static implicit operator EntityCreateArgs(string value)
        {
            return new EntityCreateArgs(value);
        }
    }
}
