using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class Crosshair : AnimatedSprite, IMediatorClient
    {
        const string path = "../../res/ui/crosshair_0.png";

        private IMediator mediator;

        public override int SortingLayer => base.SortingLayer + 1;

        public Crosshair() 
            : base(Image.FromFile(path), new Vector2(-100, -100), new Vector2(32, 32), 2, 2)
        {
            FrameAnimation anim = new FrameAnimation(0.5f, true, 0, 4);
            animator.AddAnimation("pulse", anim);
            animator.PlayAnimation("pulse");
        }

        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            mediator.Notify(this, "update");
        }
    }
}
