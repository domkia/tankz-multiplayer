using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TerrainScene : Scene
    {
        private Terrain terrain;

        public override void Load()
        {
            terrain = new Terrain();
            CreateEntity(terrain);
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.SkyBlue);
            base.Render(context);
        }
    }
}
