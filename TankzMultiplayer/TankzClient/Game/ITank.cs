using TankzClient.Framework;

namespace TankzClient.Game
{
    interface ITank
    {
        float Angle { get; }
        float Power { get; }
        int Fuel { get; }
        Vector2 Position { get; }

        void SetAngle(float angle);
        void SetPower(float power);
        void SetFuel(int fuel);
        void Move(Vector2 offset);

        void Shoot();
    }
}
