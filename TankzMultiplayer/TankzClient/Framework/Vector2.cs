
namespace TankzClient.Framework
{
    public struct Vector2
    {
        public float x;
        public float y;

        public float Magnitude { get { return (float)System.Math.Sqrt(x * x + y * y); } }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 up => new Vector2(0.0f, 1.0f);
        public static Vector2 down => new Vector2(0.0f, -1.0f);
        public static Vector2 right => new Vector2(1.0f, 0.0f);
        public static Vector2 left => new Vector2(-1.0f, 0.0f);

        public void Normalize()
        {
            float magnitude = Magnitude;
            x /= magnitude;
            y /= magnitude;
        }

        #region Operators
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            a.x += b.x;
            a.y += b.y;
            return a;
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            a.x -= b.x;
            a.y -= b.y;
            return a;
        }

        public static Vector2 operator *(Vector2 a, float factor)
        {
            a.x *= factor;
            a.y *= factor;
            return a;
        }

        public static Vector2 operator /(Vector2 a, float factor)
        {
            a.x /= factor;
            a.y /= factor;
            return a;
        }
        #endregion

        // Dot product
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        // Cross product
        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        // Distance between two points
        public static float Distance(Vector2 a, Vector2 b)
        {
            Vector2 dir = b - a;
            return dir.Magnitude;
        }

        // Get Interpolated point
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            Vector2 dir = b - a;
            float dist = dir.Magnitude;
            dir.Normalize();
            return a + dir * (dist * t);
        }

        // Calculate angle between vectors
        // Angle is in degrees
        public static float Angle(Vector2 a, Vector2 b)
        {
            float dot = Dot(a, b);
            float am = a.Magnitude;
            float bm = b.Magnitude;
            double radians = System.Math.Acos(dot / (am * bm));
            return (float)(radians / System.Math.PI) * 180.0f;
        }
    }
}
