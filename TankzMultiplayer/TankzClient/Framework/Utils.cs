using System;

namespace TankzClient.Framework
{
    public static class Utils
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(0f, 1f, t);
        }

        public static float Clamp(float min, float max, float value)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            return value;
        }

        public static double Rad2Deg(float radians)
        {
            double degrees = radians / Math.PI * 180.0;
            return degrees;
        }

        public static double Deg2Rad(float degrees)
        {
            double radians = degrees / 180.0 * Math.PI;
            return radians;
        }
    }
}
