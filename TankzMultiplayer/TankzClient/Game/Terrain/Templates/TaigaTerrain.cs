using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Game
{
    public class TaigaTerrain : TerrainTemplate
    {
        private Random random = new Random();

        public override void SetBaseColor()
        {
            Color topColor = Color.FromArgb(43, 193, 107);
            Color bottomColor = Color.FromArgb(38, 172, 96);
            LinearGradientBrush brush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 600),
                bottomColor,
                topColor
            );
            terrain.BaseColor = brush;
        }

        public override void SetTopColor()
        {
            Color grassColor = Color.FromArgb(46, 204, 113);
            terrain.TopColor = grassColor;
        }
    }
}
