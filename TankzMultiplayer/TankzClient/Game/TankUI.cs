using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class TankUI : UIElement, IMediator
    {
        private Tank tank;
        private Crosshair crosshair;

        // Progress bars
        private ProgressBar healthBar;
        private ProgressBar fuelBar;
        private ProgressBar powerBar;
        private ProgressBar angleBar;

        // Colors
        private readonly Color backgroundColor = Color.FromArgb(23, 46, 54);
        private readonly Color barBackgroundColor = Color.FromArgb(36, 76, 90);
        private readonly Color healthbarColor = Color.FromArgb(255, 96, 167);
        private readonly Color fuelColor = Color.FromArgb(124, 155, 255);
        private readonly Color angleColor = Color.FromArgb(229, 124, 255);
        private readonly Color powerColor = Color.FromArgb(194, 255, 97);

        // Images and Icons
        private AnimatedSprite icons;
        private Image shadow;

        private Font font;
        private SolidBrush bgBrush;

        public TankUI(PlayerTank tank) : base(new Rectangle(0, 0, 800, 600))
        {
            this.tank = tank;

            // Load images and icons
            icons = new AnimatedSprite(Image.FromFile("../../res/ui/ui_icons.png"), new Vector2(), new Vector2(32, 32), 2, 2);
            shadow = Image.FromFile("../../res/ui/fade_vert.png");

            // Initialize progress bars
            healthBar = new ProgressBar(new Rectangle(86, 532, 128, 14), healthbarColor, barBackgroundColor);
            fuelBar = new ProgressBar(new Rectangle(264, 532, 128, 14), fuelColor, barBackgroundColor);
            angleBar = new ProgressBar(new Rectangle(442, 532, 128, 14), angleColor, barBackgroundColor);
            powerBar = new ProgressBar(new Rectangle(620, 532, 128, 14), powerColor, barBackgroundColor);

            font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
            bgBrush = new SolidBrush(backgroundColor);

            // Create crosshair
            crosshair = new Crosshair();
            crosshair.SetMediator(this);
            SceneManager.Instance.CurrentScene.CreateEntity(crosshair);
        }

        public override void Click(Point mousePos) { }

        public void Notify(Entity sender, string action)
        {
            if (sender is Tank)
            {
                switch (action)
                {
                    case "damage":
                        healthBar.SetProgress(tank.State.Health);
                        break;
                    case "fuel":
                        float fuelRatio = tank.Fuel / 100.0f;
                        fuelBar.SetProgress(fuelRatio);
                        break;
                    case "angle":
                        float angleRatio = tank.Angle / 180.0f;
                        angleBar.SetProgress(angleRatio);
                        break;
                    case "power":
                        powerBar.SetProgress(tank.Power);
                        break;
                }
            }
            else if (sender is Crosshair)
            {
                switch (action)
                {
                    case "update":
                        Vector2 pos = tank.Barrel.GetReleasePosition();
                        Vector2 dir = pos - tank.Barrel.transform.position;
                        dir.Normalize();
                        crosshair.transform.SetPosition(pos + dir * tank.Power * 100.0f);
                        break;
                }
            }
        }

        public override void Render(Graphics context)
        {
            Notify(this, "fuel");
            RectangleF r = context.ClipBounds;

            // draw shadow
            context.DrawImage(shadow, 0, r.Bottom - 140, 100, 100);

            // bottom panel
            context.FillRectangle(bgBrush, 0, r.Bottom - 64, 800, 64);

            // health
            icons.SetFrame(0);
            icons.transform.SetPosition(new Vector2(68, r.Bottom - 24));
            icons.Render(context);
            healthBar.Render(context);
            context.DrawString("HEALTH", font, Brushes.White, new Point(120, (int)(r.Bottom - 48)));

            // fuel
            icons.SetFrame(1);
            icons.transform.SetPosition(new Vector2(244, r.Bottom - 24));
            icons.Render(context);
            fuelBar.Render(context);
            context.DrawString("FUEL", font, Brushes.White, new Point(300, (int)(r.Bottom - 48)));

            // angle
            icons.SetFrame(2);
            icons.transform.SetPosition(new Vector2(424, r.Bottom - 24));
            icons.Render(context);
            angleBar.Render(context);
            context.DrawString("ANGLE", font, Brushes.White, new Point(472, (int)(r.Bottom - 48)));

            // power
            icons.SetFrame(3);
            icons.transform.SetPosition(new Vector2(604, r.Bottom - 24));
            icons.Render(context);
            powerBar.Render(context);
            context.DrawString("POWER", font, Brushes.White, new Point(660, (int)(r.Bottom - 48)));

            // draw icon
            context.FillRectangle(Brushes.Gray, 10, r.Bottom - 128, 80, 80);
        }
    }
}
