using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameClasses.GameObjects;
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
        background = new GameObject(backgroundTexture, Color.White, window);

        Texture2D titleTexture = content.Load<Texture2D>("ObjectTextures/Tomb_Runner");
        title = new GameObject(titleTexture, Color.White, titleRect);

        Texture2D buttonPressed = content.Load<Texture2D>("ObjectTextures/Button_Pressed");
        Texture2D buttonUnpressed = content.Load<Texture2D>("ObjectTextures/Button_Unpressed");
        SpriteFont earthrealm12 = content.Load<SpriteFont>("Fonts/Earthrealm12");

        playButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Play"
        };
        playButton.Click += PlayButton_Click;

        buttonRect.Offset(0, (int)(buttonSize.Y * 1.25));
        optionsButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Options"
        };
        optionsButton.Click += OptionsButton_Click;

        buttonRect.Offset(0, (int)(buttonSize.Y * 1.25));
        exitButton = new Button(buttonUnpressed, earthrealm12, buttonRect, Color.White, Color.White)
        {
            HoverTexture = buttonPressed,
            Text = "Exit"
        };
        exitButton.Click += ExitButton_Click;

        objects.AddRange(new GameObject[]{playButton, optionsButton, exitButton, title});
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
        background.Draw(_spriteBatch);
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Draw(_spriteBatch);
        }
    }
    #endregion
}