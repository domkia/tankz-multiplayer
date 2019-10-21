namespace TankzClient.Framework
{
    public interface IAnimator
    {
        void AddAnimation(string name, IAnimationData animation);
        void PlayAnimation(string name);
        bool IsAnimationPlaying(string name);
        void StopAnimation();

        void Update(float deltaTime, AnimatedSprite sprite);
    }
}
