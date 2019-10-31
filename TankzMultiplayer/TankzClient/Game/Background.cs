using System.Drawing;
using System.Drawing.Drawing2D;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class Background : Entity, IRenderable
    {
        const string backgroundPath = "../../res/background/background_0.png";
        private Image background;

        public int SortingLayer => -10;

        public Matrix OrientationMatrix => transform.OrientationMatrix;

        public Background()
        {
            background = Image.FromFile(backgroundPath);
        }
        
        public void Render(Graphics context)
        {
            context.DrawImage(background, 0, 0, 800, 600);
        }
    }
}
