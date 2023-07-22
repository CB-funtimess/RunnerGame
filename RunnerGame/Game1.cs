using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameClasses.Managers;

namespace RunnerGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ScreenManager _sManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        
        _sManager = new ScreenManager(this)
        {
            WindowDimensions = new Rectangle(new Point(0,0), new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight))
        };
    }

    protected override void Initialize()
    {
        Window.Title = "Tomb Runner";
        _graphics.PreferMultiSampling = false;
        GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
        _graphics.ApplyChanges();
        
        _sManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sManager.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _sManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.Deferred,
        BlendState.AlphaBlend,
        SamplerState.PointClamp,
        null, null, null, null);

        _sManager.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
