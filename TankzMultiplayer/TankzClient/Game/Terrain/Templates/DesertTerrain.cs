using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class DesertTerrain : TerrainTemplate
    {
        private Random random = new Random();

        public override void SetBaseColor()
        {
            Color sandColor = Color.FromArgb(250, 190, 120);
            LinearGradientBrush brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 600),
                sandColor,
                sandColor
            );
            terrain.BaseColor = brush;
        }

        public override void SetTopColor()
        {
            terrain.TopColor = Color.SandyBrown;
        }

        public override void SpawnProps()
        {
            const int numProps = 4;
            Random random = new Random();
            for (int i = 0; i < numProps; i++)
            {
                int x = random.Next(800);
                int y = terrain.GetTerrainHeightAt(x);

                // Spawn cactus at x y
                string image = string.Format($"../../res/scenery/cactus_{random.Next(0, 2)}.png");
                Sprite cactus = new Sprite(Image.FromFile(image), new Vector2(x, y - 20), new Vector2(32, 64));
                SceneManager.Instance.CurrentScene.CreateEntity(cactus);
            }
        }

        public override bool TopLayerStyle(int depth)
        {
            int width = terrain.GrassWidth;
            bool noise = random.NextDouble() > (depth / (double)width);
            return noise;
        }
    }
}
