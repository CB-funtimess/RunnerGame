using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Engine.GameObjects;

public class ChangeTextureObj : GameObject
{
    #region Fields
    Texture2D[] textures;
    #endregion

    #region Methods
    public ChangeTextureObj(Texture2D[] textures, Color colour, Rectangle objectRectangle) : base(textures[0], colour, objectRectangle)
    {
        this.textures = textures;
    }

    public void ChangeTexture(int index)
    {
        try
        {
            texture = textures[index];
        }
        catch (System.Exception)
        {
            return;
        }
    }
    #endregion
}
