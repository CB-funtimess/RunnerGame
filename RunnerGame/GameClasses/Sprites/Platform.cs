using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Sprites;

namespace GameClasses.Sprites;

public class Platform : Sprite
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Methods
    public Platform(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration, bool doesDamageToPlayer) : base(colour, size, velocity, acceleration, doesDamageToPlayer)
    {
        IsPlatform = true;
    }
    #endregion
}