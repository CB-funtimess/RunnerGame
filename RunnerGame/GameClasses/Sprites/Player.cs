using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Sprites;

namespace GameClasses.Sprites;

public class Player : Sprite
{
    #region Fields
    private Animation runAnimation;
    private Animation idleAnimation;
    private Animation deathAnimation;
    private const int speed = 20;
    #endregion

    #region Properties
    public bool IsRunning { get; protected set; }
    public bool IsJumping { get; protected set; }
    public int Lives { get; private set; }
    #endregion

    #region Methods
    public Player(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration) : base(colour, size, velocity, acceleration, false)
    {
        Lives = 3;
        IsRunning = false;
        IsJumping = false;
    }

    public void BeginRunning(int direction)
    {
        IsRunning = true;
        Velocity = new Vector2(speed * direction, Velocity.Y);
    }

    public void EndRunning()
    {
        IsRunning = false;
        if (!IsJumping)
        {
            Velocity = new Vector2(0);
            Acceleration = new Vector2(0);
        }
    }

    public void BeginJumping()
    {
        IsJumping = true;
    }

    public void EndJumping()
    {
        IsJumping = false;
    }

    public void InitialiseAnimations(ContentManager content)
    {
        Texture2D runSheet = content.Load<Texture2D>("SpriteSheets/Run-Sheet-Player");
        Texture2D idleSheet = content.Load<Texture2D>("SpriteSheets/Idle-Sheet-Player");
        Texture2D deathSheet = content.Load<Texture2D>("SpriteSheets/Death-Sheet-Player");
        int lifespan = 20;

        runAnimation = new Animation(runSheet);
        deathAnimation = new Animation(deathSheet, false);
        for (int i = 1; i <= 6; i++)
        {
            Rectangle runSource = new Rectangle(new Point(16 + 64 * (i - 1), 32), new Point(32, 32));
            runAnimation.AddFrame(runSource, 10);
            Rectangle deathSource = new Rectangle(new Point(48 * (i - 1), 0), new Point(32));
            deathAnimation.AddFrame(deathSource, lifespan);
        }
        idleAnimation = new Animation(idleSheet);
        for (int i = 1; i <= 4; i++)
        {
            Rectangle idleSource = new Rectangle(new Point(32 * (i - 1), 0), new Point(32));
            idleAnimation.AddFrame(idleSource, lifespan);
        }
    }

    public override void Draw(SpriteBatch _spriteBatch)
    {
        if (Lives == 0)
        {
            _inUseAnimation = deathAnimation;
        }
        else if (IsRunning)
        {
            _inUseAnimation = runAnimation;
        }
        else
        {
            _inUseAnimation = idleAnimation;
        }

        base.Draw(_spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        runAnimation.Update(gameTime);
        idleAnimation.Update(gameTime);
        deathAnimation.Update(gameTime);
    }

    #endregion


}
