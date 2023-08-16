using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.GameStates;
using GameClasses.Sprites;
using Engine.Sprites;
using Microsoft.Xna.Framework.Input;
using System.Security.Principal;
using GameClasses.GeneralClasses;

namespace GameClasses.GameStates;

public class PlayState : GameState
{
    #region Fields
    private bool paused;
    private Player p1;
    private List<Sprite> sprites;
    private List<GameObject> gameObjects;

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
    }

    public override void Initialize()
    {
        Rectangle p1Rect = new Rectangle(new Point(1280, 720), new Point(96, 96));
        p1 = new Player(Color.White, p1Rect, new Vector2(0,0), new Vector2(0,400));
        sprites = new List<Sprite>();
        gameObjects = new List<GameObject>();
    }

    public override void LoadContent()
    {
        p1.InitialiseAnimations(content);

        // Initialise sprites here
        sprites = Sorting.SortByDrawOrder(sprites.ToArray()).ToList();

        Rectangle window = game.Window.ClientBounds;
        Texture2D generalBackgroundTexture = content.Load<Texture2D>("Backgrounds/Medium_Dark_Cave_Rocks_Background");
        GameObject background = new GameObject(generalBackgroundTexture, Color.White, window)
        {
            DrawOrder = 10
        };

        Point lightSize = new Point(window.Width, window.Height / 9); //https://imageonline.co/repeat-image-generator.php
        Rectangle lightRect = new Rectangle(new Point(0, window.Bottom - lightSize.Y), lightSize);
        Texture2D lightBackgroundTexture = content.Load<Texture2D>("Backgrounds/Cave_Rocks_Background");
        GameObject lightBottomBackground = new GameObject(lightBackgroundTexture, Color.White, lightRect)
        {
            DrawOrder = 9
        };
        Rectangle lightTopRect = new Rectangle(new Point(0,0), lightSize);
        GameObject lightTopBackground = new GameObject(lightBackgroundTexture, Color.White, lightTopRect)
        {
            DrawOrder = 9
        };

        gameObjects.AddRange(new GameObject[]{background, lightBottomBackground, lightTopBackground});
        gameObjects = Sorting.SortByDrawOrder(gameObjects.ToArray()).ToList();
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
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].Update(gameTime);
        }
        p1.Update(gameTime);

        // Collision handling comes after sprites have been updated, so that their positions can be correctly modified
    }

    public void HandleCollisions()
    {
        // Find collisions between 
        for (int i = 0; i < sprites.Count; i++)
        {
            if (Collisions.RectanglesColliding(p1.ObjectRectangle, sprites[i].ObjectRectangle))
            {
                if (sprites[i].IsPlatform)
                {
                    if (p1.CurrentVelocity.Y >= 0) // If sprite is moving down
                    {
                        
                    }
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].Draw(_spriteBatch);
        }
        for (int i = 0; i < sprites.Count; i++)
        {
            sprites[i].Draw(_spriteBatch);
        }
        p1.Draw(_spriteBatch);
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    private void ProcessKeyboardInput()
    {
        Keys[] pressedKeys = currentState.GetPressedKeys();
        if (!pressedKeys.Contains(Keys.A) || !pressedKeys.Contains(Keys.D))
        {
            p1.EndRunning();
        }
        if (pressedKeys.Contains(Keys.A) && !pressedKeys.Contains(Keys.D))
        {
            p1.BeginRunning(-1);
        }
        else if (pressedKeys.Contains(Keys.D) && !pressedKeys.Contains(Keys.A))
        {
            p1.BeginRunning(1);
        }
        if (pressedKeys.Contains(Keys.W) && !p1.IsJumping) // This needs finishing
        {
            p1.BeginJumping();
        }

        if (pressedKeys.Contains(Keys.Escape))
        {
            game.Exit();
        }
    }
    #endregion
}