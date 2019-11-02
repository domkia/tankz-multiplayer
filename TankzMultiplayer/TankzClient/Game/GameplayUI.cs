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

    class GameplayUI : Entity, IRenderable
    {
        private ProgressBar healthBar;
        private ProgressBar fuel;
        private ProgressBar power;
        private ProgressBar angle;
        private Font font;
        private Pen pen;
        private Pen borderPen;

        public GameplayUI()
        {
            healthBar = new ProgressBar(new Rectangle(10, 411, 140, 14), Color.LightGreen, Color.DarkGreen);
            healthBar.SetParent(this);
            healthBar.SetProgress(1f);

            fuel = new ProgressBar(new Rectangle(240, 510, 80, 10), Color.LightGreen, Color.DarkGreen);
            fuel.SetParent(this);
            fuel.SetProgress(0.9f);

            power = new ProgressBar(new Rectangle(347, 510, 80, 10), Color.LightGreen, Color.DarkGreen);
            power.SetParent(this);
            power.SetProgress(0.8f);

            angle = new ProgressBar(new Rectangle(447, 510, 80, 10), Color.LightGreen, Color.DarkGreen);
            angle.SetParent(this);
            angle.SetProgress(0.4f);

            font = new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold);
            pen = new Pen(Color.Bisque, 8);
            borderPen = new Pen(Color.DarkRed, 4);
            pen.StartCap = LineCap.ArrowAnchor;
        }

        public int SortingLayer => 9;

        public Matrix OrientationMatrix => transform.OrientationMatrix;

        public void Render(Graphics context)
        {
            context.FillRectangle(Brushes.Brown, 160, 479, 624, 80); //bottom rect
            context.DrawRectangle(borderPen, 160, 479, 624, 80); //bottom rect

            context.DrawString("FUEL", font, Brushes.Bisque, new Point(250, 485));
            context.DrawString("90", font, Brushes.Bisque, new Point(260, 520));

            context.DrawString("POWER", font, Brushes.Bisque, new Point(350, 485));
            context.DrawString("80", font, Brushes.Bisque, new Point(370, 520));

            context.DrawString("ANGLE", font, Brushes.Bisque, new Point(450, 485));
            context.DrawString("-18°", font, Brushes.Bisque, new Point(460, 520));


            context.FillRectangle(Brushes.Chocolate, 644, 421, 138, 138); //bottom right rect
            context.DrawRectangle(borderPen, 644, 421, 138, 138); //bottom right rect border
            context.FillRectangle(Brushes.Chocolate, 1, 400, 160, 160); //bottom left rect
            context.DrawRectangle(borderPen, 1, 400, 160, 160); //bottom left rect

            context.FillRectangle(Brushes.Brown, 240, 2, 300, 45); //turn
            context.DrawRectangle(borderPen, 240, 2, 300, 45); //turn
            context.DrawString("TURN: " + NetworkManager.Instance.getCurrentPlayer(), font, Brushes.Bisque, new Point(255, 10));

            context.FillRectangle(Brushes.Brown, 2, 2, 155, 70); // wind
            context.DrawRectangle(borderPen, 2, 2, 155, 70); // wind
            context.DrawString("WIND: " + "100", font, Brushes.Bisque, new Point(15, 10));
        }
    }
}
