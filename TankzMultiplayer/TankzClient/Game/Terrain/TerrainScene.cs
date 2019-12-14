using System.Collections.Generic;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class TerrainScene : Scene
    {
        private Terrain terrain;

        public override void Load()
        {
            // Desert terrain
            //TerrainTemplate template = new DesertTerrain();

            // Taiga terrain
            TerrainTemplate template = new TaigaTerrain();
            terrain = template.GenerateTerrain();
            CreateEntity(terrain);
        }

        // for testing purposes
        List<Vector2> points = new List<Vector2>();

        public override void Update(float deltaTime)
        {
            if (Input.MouseButtonDown)
            {
                Vector2 pos = Input.MousePosition;
                int y = terrain.GetTerrainHeightAt((int)pos.x);
                points.Add(new Vector2(pos.x, y));
            }
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            for (int i = 0; i < points.Count; i++)
            {
                context.FillPie(Brushes.Red, new Rectangle((int)points[i].x - 10, (int)points[i].y - 10, 20, 20), 0f, 360f);
            }
        }
    }
}
