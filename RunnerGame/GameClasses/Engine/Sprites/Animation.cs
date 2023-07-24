using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Engine.Sprites;

public class Animation
{
    #region Fields
    private List<AnimationFrame> frames = new List<AnimationFrame>();
    private int animationAge = 0;
    private int lifespan = -1;
    private bool isLoop;
    #endregion

    #region Properties
    public int Lifespan
    {
        get
        {
            if (lifespan < 0)
            {
                foreach (var frame in frames)
                {
                    lifespan += frame.Lifespan;
                }
            }
            return lifespan;
        }
    }
    public AnimationFrame CurrentFrame
    {
        get
        {
            AnimationFrame currentFrame = null;
            int tempSpan = 0;
            foreach (var frame in frames)
            {
                if (tempSpan + frame.Lifespan >= animationAge)
                {
                    currentFrame = frame;
                    break;
                }
                else
                {
                    tempSpan += frame.Lifespan;
                }
            }

            return currentFrame ?? frames.LastOrDefault();
        }
    }
    #endregion

    #region Methods
    public Animation(bool looping=false)
    {
        isLoop = looping;
    }

    public void AddFrame(Rectangle sourceRectangle, int lifespan)
    {
        frames.Add(new AnimationFrame(sourceRectangle, lifespan));
    }

    public void Update(GameTime gameTime)
    {
        animationAge = (animationAge+1) % Lifespan;
    }

    public void Reset()
    {
        animationAge = 0;
    }
    #endregion
}