using System.Drawing;
using TankzClient.Framework.Components;

namespace TankzClient.Framework
{
    /// <summary>
    /// Animated sprite
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        public int horSpan { get; private set; }
        public int vertSpan { get; private set; }
        private Vector2 frameSize;
        private Vector2 frameOffset;

        public AnimatedSprite(Image image, Vector2 position, Vector2 size, int horSpan, int vertSpan) 
            : base(image, position, size)
        {
            this.horSpan = horSpan;
            this.vertSpan = vertSpan;
            frameOffset = new Vector2(0, 0);
            frameSize = new Vector2(image.Width / horSpan, image.Height / vertSpan);
            AddComponent<AnimationComponent>();

            SetFrame(0);
        }

        public override void Render(Graphics context)
        {
            TransformComponent transform = GetComponent<TransformComponent>();
            context.DrawImage(image, transform.Rect, frameOffset.x, frameOffset.y, frameSize.x, frameSize.y, GraphicsUnit.Pixel);
        }

        public void SetFrame(int index)
        {
            // Make sure index is valid
            if (index < 0 || index >= horSpan * vertSpan)
                throw new System.IndexOutOfRangeException();

            frameOffset.x = index % horSpan * frameSize.x;
            frameOffset.y = index / horSpan * frameSize.y;
        }
    }
}
