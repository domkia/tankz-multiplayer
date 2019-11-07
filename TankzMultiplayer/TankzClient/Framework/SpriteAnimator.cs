using System;
using System.Collections.Generic;

namespace TankzClient.Framework
{
    /// <summary>
    /// Sprite frame animation component.
    /// Supports only NxM spritesheets
    /// </summary>
    public class SpriteAnimator : IAnimator
    {
        // Dictionary of animations we can access by their name
        private Dictionary<string, FrameAnimation> animations;

        private FrameAnimation currentAnimation;
        public FrameAnimation CurrentAnimation => currentAnimation;

        private bool isPlaying;
        public bool IsPlaying => isPlaying;

        private float timer;

        public SpriteAnimator()
        {
            animations = new Dictionary<string, FrameAnimation>();
            timer = 0.0f;
            isPlaying = false;
        }

        /// <summary>
        /// Add a new animation
        /// </summary>
        public void AddAnimation(string name, IAnimationData animation)
        {
            if (name == null)
                return;
            if (!animations.ContainsKey(name))
            {
                FrameAnimation frameAnimation = animation as FrameAnimation;
                animations.Add(name, frameAnimation);
                if (animations.Count == 1)
                {
                    currentAnimation = frameAnimation;
                }
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
        public bool IsAnimationInList(string name)
        {
            return animations.ContainsKey(name);
        }

        public bool IsAnimationPlaying(string name)
        {
            return CurrentAnimation == animations[name] && isPlaying;
        }

        public void StopAnimation()
        {
            isPlaying = false;
        }

        public void Update(float deltaTime, AnimatedSprite animatedSprite)
        {
            if (isPlaying == false || animations.Count == 0)
                return;

            if (currentAnimation == null)
                throw new Exception("Current animation is null");

            // Increment timer
            timer += deltaTime;

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
            animatedSprite.SetFrame(currFrame);
        }
    }
}
