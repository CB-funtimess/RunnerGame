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
    #endregion

    #region Methods
    public Platform(Texture2D texture, Color colour, Rectangle size) : base(texture, colour, size)
    {
        CanCollide = true;
    }

/*
    public override void Draw(SpriteBatch _spriteBatch)
    {
        // Draws the same texture to multiple places to create a line of textures.
        double scaleFactor = dimensions.Y / texture.Height;
        Point size = new Point((int)(texture.Width * scaleFactor), dimensions.Y);
        Rectangle start = new Rectangle(TopLeftPoint.ToPoint(), size);
        for (int i = 0; i < dimensions.Y / size.Y; i++)
        {
            _spriteBatch.Draw(texture, start, colour);
            start = new Rectangle(new Point((int)(TopLeftPoint.X + (size.X * (i+1))), (int)TopLeftPoint.Y), size);
        }
    }
*/
    #endregion
    
}