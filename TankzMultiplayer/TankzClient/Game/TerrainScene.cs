using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TerrainScene : Scene
    {
        private Terrain terrain;

        private Image background;

        public override void Load()
        {
            terrain = new Terrain();
            CreateEntity(terrain);

            background = Image.FromFile("../../res/background/background_0.png");
        }

        public override void Render(Graphics context)
        {
            context.DrawImage(background, 0, 0, 800, 600);
            base.Render(context);
        }
    }
}
