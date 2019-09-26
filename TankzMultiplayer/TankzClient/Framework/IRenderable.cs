using System.Drawing;

namespace TankzClient.Framework
{
    /// <summary>
    /// Rendering interface
    /// </summary>
    public interface IRenderable
    {
        int SortingLayer { get; }
        void Render(Graphics context);
    }
}
