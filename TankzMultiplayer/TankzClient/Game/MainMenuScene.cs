using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class MainMenuScene : Scene
    {
        private Button startButton;
        private InputField inputField;
        private ProgressBar progressBar;
        private Button[] testButtons = new Button[3];

        private void StartButton_OnClickCallback()
        {
            NetworkManager.Instance.SendRequest("join;" + inputField.Text);
        }

        public override void Load()
        {
            InventoryUI inventoryUI = new InventoryUI(new Rectangle(100,100,300,300));
            CreateEntity(inventoryUI);
            Tank usaTank = new TankBuilder()
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();
            CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));

            Tank nazziTank = new TankBuilder()
                .SetChassis(3, 0)
                .SetTurret(0)
                .SetTracks(1)
                .Build();
            CreateEntity(nazziTank);
            nazziTank.transform.SetPosition(new Vector2(500, 200));

            Tank jpTank = new TankBuilder()
                .SetChassis(0, 2)
                .SetTurret(2)
                .SetTracks(2)
                .Build();
            CreateEntity(jpTank);
            jpTank.transform.SetPosition(new Vector2(500, 300));

            inputField = CreateEntity(new InputField(20, 20, 120, 20)) as InputField;
            startButton = CreateEntity(new Button(100, 100, 50, 50, null, "test")) as Button;
            startButton.OnClickCallback += StartButton_OnClickCallback;

            progressBar = CreateEntity(new ProgressBar(new Rectangle(170, 100, 64, 8), Color.LightGreen, Color.DarkGreen)) as ProgressBar;
            progressBar.SetProgress(0.4f);

            UIFactory uiFactory = new UIFactory();
            for (int i = 0; i < 3; i++)
            {
                testButtons[i] = uiFactory.Create(new UICreateArgs("button", new Vector2(200, 200 + i * 50))) as Button;
            }

            Grenade grenade = new ProjectileFactory().Create("grenade") as Grenade;
            grenade.transform.SetPosition(new Vector2(400, 100));
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.DarkBlue);
            base.Render(context);
            context.DrawString("Scene: Main Menu", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
