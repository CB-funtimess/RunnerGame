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
        paused = false;
    }

    public override void Initialize()
    {
        Point p1Size = new Point(96, 96);
        Point middle = new Point(game.Window.ClientBounds.Center.X - (p1Size.X / 2), game.Window.ClientBounds.Center.Y - (p1Size.Y / 2));
        Rectangle p1Rect = new Rectangle(middle, p1Size);
        p1 = new Player(Color.White, p1Rect, new Vector2(0,0), new Vector2(0, 500));
        sprites = new List<Sprite>();
        gameObjects = new List<GameObject>();
    }

    public override void LoadContent()
    {
        Rectangle window = game.Window.ClientBounds;

        p1.InitialiseAnimations(content);

        // Initialise sprites here
        sprites = Sorting.SortByDrawOrder(sprites.ToArray()).ToList();

        Texture2D generalBackgroundTexture = content.Load<Texture2D>("Backgrounds/Medium_Dark_Cave_Rocks_Background");
        GameObject background = new GameObject(generalBackgroundTexture, Color.White, window)
        {
            DrawOrder = 10
        };

        Point lightSize = new Point(window.Width, window.Height / 9);         
        Rectangle lightBottomRect = new Rectangle(new Point(0, window.Bottom - lightSize.Y), lightSize);
        //https://imageonline.co/repeat-image-generator.php
        Texture2D lightBackgroundTexture = content.Load<Texture2D>("Backgrounds/Cave_Rocks_Background");
        GameObject lightBottomBackground = new GameObject(lightBackgroundTexture, Color.White, lightBottomRect)
        {
            DrawOrder = 9,
            CanCollide = true
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
        if (!paused)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Update(gameTime);
            }
            p1.Update(gameTime);
    
            // Collision handling comes after sprites have been updated, so that their positions can be correctly modified
            HandleCollisions(gameTime);
        }
    }

    public void HandleCollisions(GameTime gameTime)
    {
        // Find collisions between 
        for (int i = 0; i < sprites.Count; i++)
        {
            if (Collisions.RectanglesColliding(p1.ObjectRectangle, sprites[i].ObjectRectangle))
            {
                // Inflict damage and such
            }
        }
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].CanCollide)
            {
                if (Collisions.RectanglesColliding(p1.ObjectRectangle, gameObjects[i].ObjectRectangle) && p1.CurrentVelocity.Y >= 0)// If sprite is moving down
                {
                    double changeInYPosition = Math.Abs(gameObjects[i].ObjectRectangle.Top - (p1.PreviousPosition.Y + (p1.ObjectRectangle.Height / 2)));
                    float time = (float)Collisions.SolveQuadratic(0.5f * -p1.Acceleration.Y, -p1.PreviousVelocity.Y, changeInYPosition);
                    Vector2 newPosition = p1.PreviousPosition;
                    newPosition = new Vector2(p1.CurrentPosition.X, newPosition.Y + (float)changeInYPosition);
                    p1.EndJumping(newPosition);
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
        if (!paused)
        {
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
        }

        if (pressedKeys.Contains(Keys.Escape))
        {
            game.Exit();
        }
    }
    #endregion
}