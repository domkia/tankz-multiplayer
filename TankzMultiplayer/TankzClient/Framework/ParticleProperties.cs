namespace TankzClient.Framework
{
    /// <summary>
    /// Default particle properties
    /// </summary>
    public class ParticleProperties : ICloneable<ParticleProperties>
    {
        public Range startSize;
        public Range startLifetime;
        public Range startSpeed;
        public Range startAngle;
        public float spawnRate = 1f;
        public Range sizeGrow;
        public float speedDamping = 1f;
        public Vector2 startOffset;

        public ParticleProperties() { }

        public ParticleProperties(Range startSize, Range startLifeTime, Range startSpeed, 
            Range startAngle, Range sizeGrow, float speedDaming, float spawnRate = 1f, Vector2 startOffset = new Vector2())
        {
            this.startSize = startSize;
            this.startLifetime = startLifeTime;
            this.startSpeed = startSpeed;
            this.startAngle = startAngle;
            this.spawnRate = spawnRate;
            this.sizeGrow = sizeGrow;
            this.speedDamping = speedDaming;
            this.startOffset = startOffset;
        }

        public ParticleProperties Clone()
        {
            System.Console.WriteLine("PROTOYPE ParticleProperties: Clone()");

            ParticleProperties properties = new ParticleProperties(
                startSize, startLifetime,
                startSpeed,
                startAngle,
                sizeGrow,
                speedDamping,
                spawnRate,
                startOffset);
            return properties;
        }
    }

    /// <summary>
    /// Helper struct
    /// </summary>
    public struct Range
    {
        public float min;
        public float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public static implicit operator Range(float value)
        {
            return new Range(value, value);
        }
    }
}
