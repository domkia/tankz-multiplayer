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
        private int fuel = 100;

        private TankBarrel _barrel = null;
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

        public void ApplyCamouflage(int camo)
        {
            TankChassis chassis = FindChild<TankDecorator>();
            if (chassis == null)
                chassis = FindChild<TankChassis>();
            TankCamoDecorator camoChassis = new TankCamoDecorator(camo, chassis);
            camoChassis.SetParent(this);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(camoChassis);
        }

        public void ApplyAccessory(int accessory)
        {
            TankChassis chassis = FindChild<TankDecorator>();
            if (chassis == null)
                chassis = FindChild<TankChassis>();
            TankAccessoriesDecorator accessoryChassis = new TankAccessoriesDecorator(accessory, chassis);
            accessoryChassis.SetParent(this);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(accessoryChassis);
        }

        public void ApplySideskirt(int sideskirt)
        {
            TankChassis chassis = FindChild<TankDecorator>();
            if (chassis == null)
                chassis = FindChild<TankChassis>();
            TankSideskirtDecorator sideskirtChassis = new TankSideskirtDecorator(sideskirt, chassis);
            sideskirtChassis.SetParent(this);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(sideskirtChassis);
        }

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
            if(currentPhase.GetType() != typeof(TankIdle))
            {
                if (Input.IsKeyDown(System.Windows.Forms.Keys.Space))
                {
                    Shoot();
                }
            }

            if (currentPhase != null)
                currentPhase.Update(deltaTime);
        }
    }
}
