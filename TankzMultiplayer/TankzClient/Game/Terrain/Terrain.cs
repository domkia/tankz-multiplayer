using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class Terrain : Entity, IRenderable
    {
        const int screenWidth = 800;
        const int screenHeight = 600;

        public int SortingLayer => -1;
        public Matrix OrientationMatrix => new Matrix();
        public bool IsVisible { get; set; }

        private const string craterPath = "../../res/crater_2.bmp";
        private Bitmap terrainPixels;
        private Bitmap crater;

        public LinearGradientBrush BaseColor { get; set; }
        public Color TopColor { get; set; }
        public int GrassWidth { get; set; } = 10;
        private Color scorchColor = Color.SandyBrown;
        
        private Size craterSize = new Size(128, 128);

        public Terrain(Entity parent = null) 
            : base(parent)
        {
            crater = new Bitmap(Image.FromFile(craterPath), craterSize);
        }

        public void GenerateColoredTerrain(Bitmap mask, Predicate<int> topStyle)
        {
            terrainPixels = new Bitmap(mask.Width, mask.Height, PixelFormat.Format32bppArgb);
            
            try
            {
                BitmapData source = mask.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData dest = terrainPixels.LockBits(new Rectangle(0, 0, mask.Width, mask.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Random random = new Random();

                Color bot = BaseColor.LinearColors[0];
                Color top = BaseColor.LinearColors[1];

                unsafe
                {
                    byte* d = (byte*)dest.Scan0;
                    byte* s = (byte*)source.Scan0;

                    for (int i = 0; i < terrainPixels.Height; i++)
                    {
                        float lerp = (float)(i - 200) / terrainPixels.Height;
                        if (lerp < 0.0f) lerp = 0.0f;
                        else if (lerp > 1.0f) lerp = 1.0f;

                        byte r = Convert.ToByte(bot.R * lerp + top.R * (1.0f - lerp));
                        byte g = Convert.ToByte(bot.G * lerp + top.G * (1.0f - lerp));
                        byte b = Convert.ToByte(bot.B * lerp + top.B * (1.0f - lerp));

                        for (int j = 0; j < terrainPixels.Width; j++)
                        {
                            if (i > GrassWidth 
                                && s[0 - source.Stride * GrassWidth] == 0x0
                                && s[0] > 0x0)
                            {
                                int grassRatio = 0;
                                for (int k = 1; k <= GrassWidth; k++)
                                {
                                    if (s[0 - source.Stride * k] > 0x0)
                                        grassRatio++;
                                }
                                
                                if (topStyle(grassRatio))
                                {
                                    // Grass color
                                    d[0] = TopColor.B;
                                    d[1] = TopColor.G;
                                    d[2] = TopColor.R;
                                    d[3] = 0xFF;
                                }
                                else
                                {
                                    d[0] = b;       // blue
                                    d[1] = g;       // green
                                    d[2] = r;       // red
                                    d[3] = 0xFF;    // set alpha
                                }
                            }
                            else
                            {
                                // Terrain gradient color
                                d[0] = b;   // blue
                                d[1] = g;   // green
                                d[2] = r;   // red
                                d[3] = s[0];// set alpha
                            }

                            // shift pointers by 4 bytes (4 channels per pixel)
                            s += 4;
                            d += 4;
                        }
                    }
                }

                terrainPixels.UnlockBits(dest);
                mask.UnlockBits(source);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("GenerateColoredTerrain() " + e);
            }

            terrainPixels.MakeTransparent(Color.Transparent);
        }

        /// <summary>
        /// Calculate the height at given X value
        /// </summary>
        /// <param name="x">X coordinate</param>
        public int GetTerrainHeightAt(int x)
        {
            int height = 0;
            try
            {
                BitmapData source = terrainPixels.LockBits(new Rectangle(x, 0, 1, terrainPixels.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    byte* s = (byte*)source.Scan0;
                    for (int y = 0; y < terrainPixels.Height; y++)
                    {
                        if (s[3] == 0xFF)
                        {
                            height = y;
                            break;
                        }
                        s += source.Stride;
                    }
                }
                terrainPixels.UnlockBits(source);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTerrainHeightAt() " + e);
            }

            return height;
        }

        public void Scorch(Vector2 mousePos)
        {
            try
            {
                // Handle out of screen cases by clipping the rectangle
                int minClipX = mousePos.x < 0 ? (int)-mousePos.x : 0;
                int minClipY = mousePos.y < 0 ? (int)-mousePos.y : 0;

                int clippedWidth = (int)mousePos.x + crater.Width - screenWidth;
                clippedWidth = clippedWidth > 0 ? crater.Width - clippedWidth : crater.Width;

                int clippedHeight = (int)mousePos.y + crater.Height - screenHeight;
                clippedHeight = clippedHeight > 0 ? crater.Height - clippedHeight : crater.Height;

                BitmapData source = crater.LockBits(
                    new Rectangle(
                        minClipX, 
                        minClipY, 
                        clippedWidth - minClipX, 
                        clippedHeight - minClipY), 
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                BitmapData dest = terrainPixels.LockBits(
                    new Rectangle(
                        (int)mousePos.x + minClipX, 
                        (int)mousePos.y + minClipY, 
                        clippedWidth - minClipX, 
                        clippedHeight - minClipY), 
                    ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                unsafe
                {
                    byte* s = (byte*)source.Scan0;
                    byte* d = (byte*)dest.Scan0;

                    for (int i = 0; i < source.Height; i++)
                    {
                        for (int j = 0; j < source.Width; j++)
                        {
                            // Red mask (scorch)
                            if (s[2] > 0x80)
                            {
                                d[0] = scorchColor.B;
                                d[1] = scorchColor.G;
                                d[2] = scorchColor.R;
                            }

                            // Blue mask (inside crater)
                            if (s[0] > 0x80)
                            {
                                d[3] = 0x00;

                                // Green mask (outside crater)
                                if (s[1] > 0x80)
                                {
                                    d[3] = 0x00;
                                }
                            }

                            s += 4;
                            d += 4;
                        }

                        // Go the the next scan line
                        d += dest.Stride - dest.Width * 4;
                        s += source.Stride - source.Width * 4;
                    }
                }

                crater.UnlockBits(source);
                terrainPixels.UnlockBits(dest);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Scorch() " + e);
            }
        }

        public void Render(Graphics context)
        {
            // Draw sky
            //LinearGradientBrush gradient = new LinearGradientBrush(new Rectangle(0, 0, screenWidth, screenHeight / 2), Color.Black, Color.Transparent, LinearGradientMode.Vertical);
            //context.FillRectangle(Brushes.Blue, new Rectangle(0, 0, screenWidth, screenHeight));
            //context.FillRectangle(gradient, new Rectangle(0, 0, screenWidth, screenHeight / 2));

            // Draw terrain
            context.DrawImage(terrainPixels, 0, 0);
        }

        public override void Update(float deltaTime)
        {
            if (Input.MouseButtonDown)
            {
                Vector2 mousePos = Input.MousePosition;
                mousePos.x -= crater.Width / 2;
                mousePos.y -= crater.Height / 2;
                Scorch(mousePos);
                int size = (int)((new Random().NextDouble() + 0.75) / 1.75 * 128);
                craterSize = new Size(size, size);
                crater = new Bitmap(Image.FromFile(craterPath), craterSize);
            }
        }
    }
}
