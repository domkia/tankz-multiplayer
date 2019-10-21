
namespace TankzClient.Framework
{
    /// <summary>
    /// Simple spritesheet animation data structure
    /// </summary>
    public class FrameAnimation : IAnimationData
    {
        public readonly float duration;
        public readonly bool loop;
        public readonly int startFrame;
        public readonly int frameCount;

        public FrameAnimation(float duration, bool loop, int startFrame, int frameCount)
        {
            this.duration = duration;
            this.loop = loop;
            this.startFrame = startFrame;
            this.frameCount = frameCount;
        }

        /// <summary>
        /// Calculate which frame to show in particular point in time
        /// of this animation
        /// </summary>
        /// <param name="time">Time elapsed</param>
        public int GetFrame(float time)
        {
            float progress = time / duration;
            int currFrame = (int)System.Math.Floor(progress * frameCount + startFrame);
            return currFrame;
        }
    }
}
