using System.Drawing;

namespace TankzClient.Game
{
    /// <summary>
    /// This tank decorator adds an extra thing on the back of the tank
    /// like antenna, flag etc.
    /// </summary>
    class TankAccessoriesDecorator : TankDecorator
    {
        const string path = "../../res/accessories/";

        public TankAccessoriesDecorator(int index, TankChassis tank)
            : base(tank)
        {
            System.Console.WriteLine("DECORATOR new TankAccessoriesDecorator()");
            string imageFile = string.Format($"{path}accessory_{index}.png");
            image = Image.FromFile(imageFile);
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawImage(image, new Rectangle((int)tank.transform.position.x - 48, (int)tank.transform.position.y - 32, 32, 32));
        }
    }
}
