using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.Sprites;

namespace Engine.GeneralClasses;

public class Platform : GameObject
{
    #region Fields
    #endregion

    #region Properties
    public Rectangle PreviousObjectRectangle;
    #endregion

    #region Methods
    public Platform(Texture2D texture, Color colour, Rectangle size) : base(texture, colour, size)
    {
        CanCollide = true;
    }

    public override void Update(GameTime gameTime)
    {
        PreviousObjectRectangle = ObjectRectangle;
    }
    #endregion
    
}