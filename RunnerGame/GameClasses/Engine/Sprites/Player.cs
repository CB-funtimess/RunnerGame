using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Engine.Sprites;

public class Player : Sprite
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Methods

    #endregion
    public Player(Texture2D texture, Color colour, Rectangle size) : base(texture, colour, size)
    {
    }

    public Player(Texture2D texture, Vector2 centralPosition, Color colour, Point dimensions) : base(texture, centralPosition, colour, dimensions)
    {
    }
}