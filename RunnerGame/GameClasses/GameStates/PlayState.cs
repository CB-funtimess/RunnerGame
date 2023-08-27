using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.GameStates;
using GameClasses.Sprites;
using Engine.Sprites;
using Microsoft.Xna.Framework.Input;
using GameClasses.GeneralClasses;
using Engine.GeneralClasses;

namespace GameClasses.GameStates;

public class PlayState : GameState
{
    #region Fields
    private bool paused;
    private LevelEditor editor;
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
        sprites = new List<Sprite>();
        gameObjects = new List<GameObject>();
    }

    public override void LoadContent()
    {
        Rectangle window = game.Window.ClientBounds;

        // Initialise GameObjects here
        Texture2D generalBackgroundTexture = content.Load<Texture2D>("Backgrounds/Medium_Dark_Cave_Rocks_Background");
        GameObject background = new GameObject(generalBackgroundTexture, Color.White, window)
        {
            DrawOrder = 10
        };

        Point lightSize = new Point(window.Width, window.Height / 9);         
        Rectangle lightBottomRect = new Rectangle(new Point(0, window.Bottom - lightSize.Y), lightSize);
        //https://imageonline.co/repeat-image-generator.php
        Texture2D lightBackgroundTexture = content.Load<Texture2D>("Backgrounds/Cave_Rocks_Background");
        Platform lightBottomBackground = new Platform(lightBackgroundTexture, Color.White, lightBottomRect)
        {
            DrawOrder = 9,
        };
        Rectangle lightTopRect = new Rectangle(new Point(0,0), lightSize);
        GameObject lightTopBackground = new GameObject(lightBackgroundTexture, Color.White, lightTopRect)
        {
            DrawOrder = 9
        };

        // Initialisation of game objects and sprites
        editor = new LevelEditor(window, lightTopRect, lightBottomRect, default, default);
        Texture2D platformTexture = content.Load<Texture2D>("ObjectTextures/Platform");

        Rectangle[] rectangles = editor.ReadLevelFile();
        foreach (var rectangle in rectangles)
        {
            Platform tempPlatform = new Platform(platformTexture, Color.White, rectangle);
            gameObjects.Add(tempPlatform);
        }

        gameObjects.AddRange(new GameObject[]{background, lightBottomBackground, lightTopBackground});
        gameObjects = Sorting.SortByDrawOrder(gameObjects.ToArray()).ToList();

        Point p1Size = new Point(96, 96);
        Point p1Spawn = new Point(lightBottomRect.Center.X - (p1Size.X / 2), lightBottomRect.Top - (p1Size.Y));
        Rectangle p1Rect = new Rectangle(p1Spawn, p1Size);
        p1 = new Player(Color.White, p1Rect, new Vector2(0,0), new Vector2(0, 500));
        p1.InitialiseAnimations(content);

        // Initialise sprites here
        sprites = Sorting.SortByDrawOrder(sprites.ToArray()).ToList();
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
                if (Collisions.RectanglesColliding(p1.ObjectRectangle, gameObjects[i].ObjectRectangle) && ((p1.PreviousVelocity.Y > 0 && p1.PreviousObjectRectangle.Bottom < gameObjects[i].ObjectRectangle.Top) || p1.PreviousVelocity.Y == 0))// If sprite is moving down
                {
                    double changeInYPosition = Math.Abs(gameObjects[i].ObjectRectangle.Top - (p1.PreviousPosition.Y + (p1.ObjectRectangle.Height / 2)));
                    float time = (float)Collisions.SolveQuadratic(0.5f * -p1.Acceleration.Y, -p1.PreviousVelocity.Y, changeInYPosition);
                    Vector2 newPosition = p1.PreviousPosition;
                    newPosition = new Vector2(p1.CurrentPosition.X, newPosition.Y + (float)changeInYPosition);
                    p1.EndJumping(newPosition);
                }
            }
        }
        // Ensure player stays within game bounds
        if ()
        {
            
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