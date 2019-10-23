using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class Tank : Sprite
    {
        private TankBarrel _barrel = null;
        private int fuel = 100;
        public TankBarrel barrel
        {
            get
            {
                if (_barrel == null)
                    _barrel = FindChild<TankBarrel>();
                return _barrel;
            }
        }

        private TankPhase currentPhase = null;

        public void SetPhase(TankPhase phase)
        {
            currentPhase = phase;
        }

        public void Shoot()
        {
            currentPhase = new TankIdle(this);
        }

        public Tank(Image sprite, Vector2 position, Vector2 size) 
            : base(sprite, position, size)
        {
            // Set tank phase
            currentPhase = new TankMovement(this);
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawString(string.Format($"Tank Phase: {currentPhase.GetType().Name}"), SystemFonts.MenuFont, Brushes.LightGreen, new Point(0, 50));
        }

        public override void Update(float deltaTime)
        {
            if (currentPhase.GetType() != typeof (TankIdle))
            {
                if (Input.IsKeyDown(System.Windows.Forms.Keys.Enter))
                {
                    Shoot();
                }
                if (Input.IsKeyDown(System.Windows.Forms.Keys.W))
                {
                    currentPhase = new TankWeaponSelection(this);
                }
            }
            
            if (currentPhase != null)
                currentPhase.Update(deltaTime);
            
            
            base.Update(deltaTime);
        }
    }
}
