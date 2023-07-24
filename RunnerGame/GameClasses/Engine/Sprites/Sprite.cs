using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
namespace Engine.Sprites;

public class Sprite : GameObject
{
    #region Fields
    protected Vector2 velocity;
    protected Vector2 acceleration;
    #endregion

    #region Properties
    #endregion

    #region Methods
    public Sprite(Texture2D texture, Color colour, Rectangle size) : base(texture, colour, size)
    {
    }

    public Sprite(Texture2D texture, Vector2 centralPosition, Color colour, Point dimensions) : base(texture, centralPosition, colour, dimensions)
    {
    }
    #endregion
}