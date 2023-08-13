using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.GameStates;
using GameClasses.Sprites;
using Engine.Sprites;
using Microsoft.Xna.Framework.Input;

namespace GameClasses.GameStates;

public class PlayState : GameState
{
    #region Fields
    private bool paused;
    private Player p1;
    private List<Sprite> sprites;

    private KeyboardState previousState;
    private KeyboardState currentState;
    #endregion

    #region Properties
    #endregion

    #region Methods
    public PlayState(Game game)
    {
        this.game = game;
        content = game.Content;
        sprites = new List<Sprite>();
    }
    public override void Initialize()
    {
        Rectangle p1Rect = new Rectangle(new Point(1280, 720), new Point(128, 128));
        p1 = new Player(Color.White, p1Rect, new Vector2(0,0), new Vector2(0,0));
    }

    public override void LoadContent()
    {
        p1.InitialiseAnimations(content);

        sprites.Add(p1);
    }

    public override void Update(GameTime gameTime)
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
        ProcessKeyboardInput();

        for (int i = 0; i < sprites.Count; i++)
        {
            sprites[i].Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            sprites[i].Draw(_spriteBatch);
        }
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    private void ProcessKeyboardInput()
    {
        Keys[] pressedKeys = currentState.GetPressedKeys();
        if (!(pressedKeys.Contains(Keys.A) || pressedKeys.Contains(Keys.D)))
        {
            p1.EndRunning();
        }
        if (pressedKeys.Contains(Keys.A) && !pressedKeys.Contains(Keys.D))
        {
            p1.BeginRunning(-1);
        }
        if (pressedKeys.Contains(Keys.D) && !pressedKeys.Contains(Keys.A))
        {
            p1.BeginRunning(1);
        }
        if (pressedKeys.Contains(Keys.W))
        {
            p1.BeginJumping();
        }
    }

    #endregion
}