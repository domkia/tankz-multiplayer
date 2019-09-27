using System;
using System.Collections.Generic;

namespace TankzClient.Framework.Components
{
    /// <summary>
    /// Sprite frame animation component.
    /// Supports only NxM spritesheets
    /// </summary>
    public class AnimationComponent : IComponent
    {
        // Dictionary of animations we can access by their name
        private Dictionary<string, FrameAnimation> animations;

        private FrameAnimation currentAnimation;
        public FrameAnimation CurrentAnimation => currentAnimation;

        private bool isPlaying;
        public bool IsPlaying => isPlaying;

        private float timer;

        public AnimationComponent()
        {
            animations = new Dictionary<string, FrameAnimation>();
            timer = 0.0f;
            isPlaying = false;
        }

        /// <summary>
        /// Add a new animation
        /// </summary>
        public void AddAnimation(string name, FrameAnimation animation)
        {
            //TODO: check for the same name
            animations.Add(name, animation);
            if (animations.Count == 1)
            {
                currentAnimation = animation;
            }
        }

        /// <summary>
        /// Play attached animation with name
        /// </summary>
        /// <param name="name">Name of the animation to play</param>
        public void PlayAnimation(string name)
        {
            timer = 0.0f;
            if (animations.ContainsKey(name))
            {
                currentAnimation = animations[name];
                isPlaying = true;
            }
            else
                throw new Exception("No such animation exists");
        }

        public bool IsAnimationPlaying(string name)
        {
            return CurrentAnimation == animations[name] && isPlaying;
        }

        public void StopAnimation()
        {
            isPlaying = false;
        }

        public void Update(float deltaTime, Entity entity)
        {
            if (isPlaying == false || animations.Count == 0)
                return;

            if (currentAnimation == null)
                throw new Exception("Current animation is null");

            // Update animation timer
            if (timer >= currentAnimation.duration)
            {
                if (currentAnimation.loop)
                {
                    // Start animation over again
                    timer %= currentAnimation.duration;
                }
                else
                {
                    // Stop animation
                    timer = currentAnimation.duration;
                    isPlaying = false;
                }
            }

            // Set frame based on timer
            int currFrame = currentAnimation.GetFrame(timer);
            AnimatedSprite sprite = entity as AnimatedSprite;
            sprite.SetFrame(currFrame);

            // Increment timer
            timer += deltaTime;
        }
    }
}
