using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Engine.Sprites;

public class AnimationFrame
{
    #region Properties
    public Rectangle SourceRectangle { get; private set; }
    public int Lifespan { get; private set; }
    #endregion

    #region Methods
    public AnimationFrame(Rectangle sourceRect, int lifespan)
    {
        SourceRectangle = sourceRect;
        Lifespan = lifespan;
    }
    #endregion
}