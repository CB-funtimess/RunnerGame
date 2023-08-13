using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Engine.GameStates;

public abstract class GameState
{
    #region Fields
    protected Game game;
    protected ContentManager content;
    #endregion

    #region Methods
    public abstract void Initialize();
    public abstract void LoadContent();
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);
    public abstract void Dispose();
    #endregion
}