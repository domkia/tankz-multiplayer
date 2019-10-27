using System.Drawing;

namespace TankzClient.Game
{
    /// <summary>
    /// This decorator attaches a custom tank sideskirt
    /// </summary>
    class TankSideskirtDecorator : TankDecorator
    {
        const string path = "../../res/accessories/";

        public TankSideskirtDecorator(int index, TankChassis tank) 
            : base(tank)
        {
            string imageFile = string.Format($"{path}sideskirt_{index}.png");
            image = Image.FromFile(imageFile);
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            Rectangle rect = tank.transform.Rect;
            rect.Y += 12;
            context.DrawImage(image, rect);
        }
    }
}
