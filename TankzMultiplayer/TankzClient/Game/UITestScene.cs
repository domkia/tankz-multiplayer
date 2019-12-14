using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class UITestScene : Scene
    {
        public override void Load()
        {
            PlayerTank tank = new TankBuilder(true)
                .SetChassis(0, 1)
                .SetTurret(2)
                .SetTracks(0)
                .Build() as PlayerTank;
            CreateEntity(tank);
            tank.transform.SetPosition(new Vector2(300, 300));

            IMediator tankUI = CreateEntity(new TankUI(tank)) as IMediator;

            tank.SetMediator(tankUI);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.DarkSlateBlue);
            base.Render(context);
        }
    }
}
