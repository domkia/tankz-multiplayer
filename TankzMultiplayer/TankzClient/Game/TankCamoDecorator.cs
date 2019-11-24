using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TankzClient.Game
{
    /// <summary>
    /// This decorator overlays camouflage texture over existing chassis. 
    /// Opacity 0-255
    /// </summary>
    class TankCamoDecorator : TankDecorator
    {
        const string path = "../../res/camos/";
        private byte camoOpacity = 200;
        private Bitmap camo;

        public TankCamoDecorator(int camoId, TankChassis tank)
            : base(tank)
        {
            System.Console.WriteLine("DECORATOR new TankCamoDecorator()");

            string imageFile = string.Format($"{path}camo_{camoId}.png");
            camo = new Bitmap(Image.FromFile(imageFile), tank.image.Width, tank.image.Height);
            GenerateCamoBitmap();
        }

        private void GenerateCamoBitmap()
        {
            try
            {
                Bitmap tankBitmap = tank.image as Bitmap;

                Rectangle rect = new Rectangle(0, 0, tankBitmap.Width, tankBitmap.Height);
                BitmapData src = tankBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData dest = camo.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                unsafe
                {
                    byte* d = (byte*)dest.Scan0;
                    byte* s = (byte*)src.Scan0;

                    for (int i = 0; i < src.Height; i++)
                    {
                        for (int j = 0; j < src.Width; j++)
                        {
                            d[3] = s[3] != 0x00 ? 
                                Math.Min(camoOpacity, d[3]) : 
                                s[3];

                            s += 4;
                            d += 4;
                        }
                    }
                }

                tankBitmap.UnlockBits(src);
                camo.UnlockBits(dest);
            }
            catch (InvalidOperationException e) { }
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawImage(camo, tank.transform.Rect);
        }
    }
}
