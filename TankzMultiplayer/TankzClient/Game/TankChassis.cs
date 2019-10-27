using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankChassis : Sprite
    {
        private float leanAngle = 5f;
        private float bobFrequency = 16f;
        private float bobAmplitude = 1.2f;

        public override int SortingLayer => base.SortingLayer + 1;

        public TankChassis(Image image, Vector2 position, Vector2 size) 
            : base(image, position, size)
        {

        }

        private float timer = 0;

        public override void Update(float deltaTime)
        {
            transform.SetPosition(Vector2.up * (float)Math.Sin(timer * bobFrequency) * bobAmplitude);
            timer += deltaTime;
        }
    }
}
