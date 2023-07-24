using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.GameStates;
using GameClasses.GeneralClasses;
namespace GameClasses.GameStates;

public class MenuState : GameState
{
    #region Fields
    private GameObject background;
    private List<GameObject> objects;
    private Button playButton;
    private Button optionsButton;
    private Button exitButton;
    private GameObject title;
    #endregion

    #region Methods
    public MenuState(Game game)
    {
        this.game = game;
        content = game.Content;
    }

    public override void Initialize()
    {
        objects = new List<GameObject>();
    }

    public override void LoadContent()
    {
        Rectangle window = game.Window.ClientBounds;
        Point buttonSize = new Point(game.Window.ClientBounds.Width / 4, game.Window.ClientBounds.Height / 16);
        Point buttonPos = new Point(window.Center.X - (buttonSize.X / 2), window.Center.Y + (buttonSize.Y / 2));
        Rectangle buttonRect = new Rectangle(buttonPos, buttonSize);
        Point titleSize = new Point(buttonSize.X, (int)(buttonSize.Y * 3.75));
        Point titlePos = new Point(window.Center.X - (titleSize.X / 2), window.Center.Y - titleSize.Y);
        Rectangle titleRect = new Rectangle(titlePos, titleSize);

        Texture2D backgroundTexture = content.Load<Texture2D>("Backgrounds/Medium_Dark_Cave_Rocks_Background");
        background = new GameObject(backgroundTexture, Color.White, window)
        {
            DrawOrder = 1
        };

        Texture2D titleTexture = content.Load<Texture2D>("ObjectTextures/Tomb_Runner");
        title = new GameObject(titleTexture, Color.White, titleRect)
        {
            DrawOrder = 0,
        };

        Texture2D buttonPressed = content.Load<Texture2D>("ObjectTextures/Button_Pressed");
        Texture2D buttonUnpressed = content.Load<Texture2D>("ObjectTextures/Button_Unpressed");
        SpriteFont earthrealm12 = content.Load<SpriteFont>("Fonts/Earthrealm12");

        playButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Play",
            DrawOrder = 0,
        };
        playButton.Click += PlayButton_Click;

        buttonRect.Offset(0, (int)(buttonSize.Y * 1.25));
        optionsButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Options",
            DrawOrder = 0,
        };
        optionsButton.Click += OptionsButton_Click;

        buttonRect.Offset(0, (int)(buttonSize.Y * 1.25));
        exitButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Exit",
            DrawOrder = 0,
        };
        exitButton.Click += ExitButton_Click;

        objects.AddRange(new GameObject[]{playButton, optionsButton, exitButton, title, background});
        objects = Sorting.SortByDrawOrder(objects.ToArray()).ToList();
    }

    private void PlayButton_Click(object sender, EventArgs e)
    {

    }

    private void OptionsButton_Click(object sender, EventArgs e)
    {

    }

    private void ExitButton_Click(object sender, EventArgs e)
    {
        game.Exit();
    }

    public override void Update(GameTime gameTime)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Draw(_spriteBatch);
        }
    }
    #endregion
}