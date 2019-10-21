using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    public class TestScene : Scene
    {
        AnimatedSprite debug;
        AnimatedSprite player;

        public override void Load()
        {
            debug = CreateEntity(new AnimatedSprite(Image.FromFile("../../res/debug.jpg"), new Vector2(200, 200), new Vector2(100, 100), 10, 10)) as AnimatedSprite;
            debug.animator.AddAnimation("debug_anim", new FrameAnimation(10.0f, true, 0, 100));
            debug.animator.PlayAnimation("debug_anim");
            debug.transform.Rotate(30);

            player = CreateEntity(new AnimatedSprite(Image.FromFile("../../res/test_spritesheet.png"), new Vector2(300, 300), new Vector2(64, 64), 10, 4)) as AnimatedSprite;
            IAnimator animator = player.animator;
            animator.AddAnimation("run_down", new FrameAnimation(0.5f, true, 0, 10));
            animator.AddAnimation("run_left", new FrameAnimation(0.5f, true, 10, 10));
            animator.AddAnimation("run_up", new FrameAnimation(0.5f, true, 20, 10));
            animator.AddAnimation("run_right", new FrameAnimation(0.5f, true, 30, 10));

            animator.PlayAnimation("run_right");

            Input.OnMouseClick += Input_OnMouseClick;
        }

        private void Input_OnMouseClick(object sender, MouseArgs e)
        {
            player.transform.SetPosition(e.mousePosition);
        }

        public override void Render(Graphics context)
        {
            Random rand = new Random();
            context.Clear(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));
            context.DrawString("Scene: TEST", SystemFonts.MenuFont, Brushes.Red, new Point(0, 0));

            base.Render(context);
        }

        Vector2 direction = new Vector2(-1.0f, 0.0f);
        float roll = 0.0f;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Transform playerTransform = player.transform;
            playerTransform.SetAngle(roll);
            roll += deltaTime * 360f * direction.x;

           //Vector2 position = playerTransform.position;
           //playerTransform.SetPosition(position + direction * 4.0f);
           //if (position.x > 500)
           //    direction.x = -1.0f;
           //if (position.x < 100)
           //    direction.x = 1.0f;

            IAnimator anim = player.animator;
            if (direction.x < 0 && !anim.IsAnimationPlaying("run_left"))
                anim.PlayAnimation("run_left");
            else if (direction.x > 0 && !anim.IsAnimationPlaying("run_right"))
                anim.PlayAnimation("run_right");


        }
    }
}
