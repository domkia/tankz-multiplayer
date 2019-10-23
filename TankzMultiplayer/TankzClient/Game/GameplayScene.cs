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
    class GameplayScene : Scene
    {
        private ProgressBar healthBar;
        private ProgressBar fuel;
        private ProgressBar power;
        private Font font;
        private Pen pen;
        private Pen borderPen;
        public override void Load()
        {
            healthBar = CreateEntity(new ProgressBar(new Rectangle(10, 411, 140, 14), Color.LightGreen, Color.DarkGreen)) as ProgressBar;
            healthBar.SetProgress(1f);
            fuel = CreateEntity(new ProgressBar(new Rectangle(240, 525, 80, 10), Color.LightGreen, Color.DarkGreen)) as ProgressBar;
            fuel.SetProgress(0.9f);
            power = CreateEntity(new ProgressBar(new Rectangle(347, 525, 80, 10), Color.LightGreen, Color.DarkGreen)) as ProgressBar;
            power.SetProgress(0.8f);
            font = new Font(FontFamily.GenericMonospace, 16f, FontStyle.Bold);
            pen = new Pen(Color.Bisque, 8);
            borderPen = new Pen(Color.DarkRed, 4);
            pen.StartCap = LineCap.ArrowAnchor;

        }
        public override void Render(Graphics context)
        {
            context.Clear(Color.Bisque);
            
            context.FillRectangle(Brushes.Brown, 160, 479, 624, 80); //bottom rect
            context.DrawRectangle(borderPen, 160, 479, 624, 80); //bottom rect
            context.FillRectangle(Brushes.Chocolate, 644, 421, 138, 138); //bottom right rect
            context.DrawRectangle(borderPen, 644, 421, 138, 138); //bottom right rect border
            context.FillRectangle(Brushes.Chocolate, 1, 400, 160, 160); //bottom left rect
            context.DrawRectangle(borderPen, 1, 400, 160, 160); //bottom left rect
            context.DrawString("FUEL", font, Brushes.Bisque, new Point(250, 500));
            context.DrawString("POWER", font, Brushes.Bisque, new Point(350, 500));
            context.FillRectangle(Brushes.Brown, 240, 2, 300, 45); //turn
            context.DrawRectangle(borderPen, 240, 2, 300, 45); //turn
            context.DrawString("TURN: " + "PLAYER1", font, Brushes.Bisque, new Point(255, 10));
            context.FillRectangle(Brushes.Brown, 2, 2, 155, 70); // wind
            context.DrawRectangle(borderPen, 2, 2, 155, 70); // wind
            context.DrawString("WIND: " + "100", font, Brushes.Bisque, new Point(15, 10));
            context.DrawLine(pen, 15, 50, 140, 50);
            base.Render(context);
        }
    }
}
