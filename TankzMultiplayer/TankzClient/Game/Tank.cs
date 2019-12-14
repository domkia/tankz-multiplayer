using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Models;

namespace TankzClient.Game
{
    /// <summary>
    /// Base tank class
    /// </summary>
    public class Tank : Sprite, ITank, IMediatorClient
    {
        public int Fuel => state.Fuel;
        public float Power => state.Power;
        public float Angle => state.Angle;
        public Vector2 Position => new Vector2(state.Pos_X, state.Pos_Y);

        private TankState state;
        public TankState State => state;

        private TankBarrel _barrel = null;
        public TankBarrel Barrel
        {
            get
            {
                if (_barrel == null)
                    _barrel = FindChild<TankBarrel>();
                return _barrel;
            }
        }

        internal Tank(Vector2 position, Vector2 size) 
            : base(null, position, size)
        {
            state = new TankState();
        }

        public void UpdateTankState(TankState updatedState)
        {
            this.state = updatedState;
            transform.SetPosition(new Vector2(state.Pos_X, state.Pos_Y));
            SetAngle(state.Angle);
            SetFuel(state.Fuel);
            SetPower(state.Power);
            //TODO: SetHealth
        }

        public void Shoot()
        {
            float angle = Angle;
            float power = Power;
            NetworkManager.Instance.Shoot(angle, power);

            // Spawn shoot particle at the barrel tip point
            Vector2 particleSpawnPoint = Barrel.GetReleasePosition();
            ParticleEmitter particle = new ParticleFactory().Create("explosion") as ParticleEmitter;
            particle.transform.SetPosition(particleSpawnPoint);
            SoundsPlayer.Instance.PlaySound("shoot_1");
            particle.Emit();
        }

        public void Move(Vector2 offset)
        {
            if (state.Fuel == 0)
            {
                return;
            }
            Vector2 pos = transform.position + offset;
            NetworkManager.Instance.SetPos(pos);
            transform.SetPosition(transform.position + offset);
            float distance = offset.Magnitude;
            SetFuel(state.Fuel - (int)distance);
        }

        public virtual void SetAngle(float angle)
        {
            if (angle < 0.0f)
            {
                angle = 0.0f;
            }
            else if (angle > 180.0f)
            {
                angle = 180.0f;
            }
            Barrel.transform.SetAngle(-angle);
            state.Angle = angle;
            ui?.Notify(this, "angle");
        }

        public virtual void SetPower(float power)
        {
            if (power < 0.0f)
            {
                power = 0.0f;
            }
            else if (power > 1.0f)
            {
                power = 1.0f;
            }
            state.Power = power;
            ui?.Notify(this, "power");
        }

        public virtual void SetFuel(int fuel)
        {
            if (fuel < 0)
            {
                fuel = 0;
            }
            else if (fuel > 100)
            {
                fuel = 100;
            }
            state.Fuel = fuel;
            ui?.Notify(this, "fuel");
        }

        public override void Render(Graphics context)
        {
            RectangleF rect = new RectangleF(transform.position.x, transform.position.y, 84, 128);
            rect.X -= 24;
            rect.Y += 36;
            context.DrawString($"Health: {state.Health}", SystemFonts.CaptionFont, Brushes.Cyan, rect.Location);
            rect.Y += 14;
            context.DrawString($" Angle: {state.Angle}°", SystemFonts.CaptionFont, Brushes.Orange, rect.Location);
            rect.Y += 14;
            context.DrawString($" Power: {state.Power}", SystemFonts.CaptionFont, Brushes.LightPink, rect.Location);
            rect.Y += 14;
            context.DrawString($"  Fuel: {state.Fuel}", SystemFonts.CaptionFont, Brushes.LightGray, rect.Location);
        }

        protected IMediator ui;

        public void SetMediator(IMediator mediator)
        {
            this.ui = mediator;
        }
    }
}
