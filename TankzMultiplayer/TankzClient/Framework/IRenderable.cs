using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    /// <summary>
    /// Rendering interface
    /// </summary>
    public interface IRenderable
    {
        int SortingLayer { get; }
        Matrix OrientationMatrix { get; }
        void Render(Graphics context);
    }
}
