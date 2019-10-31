namespace TankzClient.Framework
{
    /// <summary>
    /// Base particle struct
    /// </summary>
    public class Particle : ICloneable<Particle>
    {
        public Vector2 position;
        public Vector2 direction;

        public float size = 1f;
        public float speed = 1f;
        public float lifetime = 1f;
        public float sizeGrow = 0f;
        public float speedDamping = 1f;

        public readonly float startSize;
        public readonly float startSpeed;

        public float timer;

        public bool IsAlive => timer > 0f && timer < lifetime;
        public float Progress => timer / lifetime;

        public Particle(float startSize, float startSpeed)
        {
            this.startSize = startSize;
            this.size = startSize;
            this.startSpeed = startSpeed;
            this.speed = startSpeed;
            this.timer = 0f;
        }

        public Particle Clone()
        {
            Particle particle = new Particle(
                startSize, 
                startSpeed
                );
            return particle;
        }
    }
}