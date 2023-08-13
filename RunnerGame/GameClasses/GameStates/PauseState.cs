using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.GameStates;
namespace GameClasses.GameStates;

public class PauseState : GameState
{
    public PauseState(Game game)
    {
        this.game = game;
        content = game.Content;
    }

    public override void Initialize()
    {

    }

    public override void LoadContent()
    {

    }

    public override void Update(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
    {

    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}