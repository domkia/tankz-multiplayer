namespace TankzClient.Framework
{
    public static class Utils
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
