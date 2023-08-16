using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Sprites;
using GameClasses.GeneralClasses;


namespace GameClasses.Sprites;

public class Player : Sprite
{
    #region Fields
    private Animation runAnimation;
    private Animation idleAnimation;
    private Animation deathAnimation;
    private const int speed = 100;
    #endregion

    #region Properties
    public bool IsRunning { get; protected set; }
    public bool IsJumping { get; protected set; }
    public int Lives { get; private set; }
    #endregion

    #region Methods
    public Player(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration) : base(colour, size, velocity, acceleration, false)
    {
        Enabled = true;
        Lives = 3;
        IsRunning = false;
        IsJumping = false;
    }

    public void BeginRunning(int direction)
    {
        IsRunning = true;
        CurrentVelocity = new Vector2(speed * direction, CurrentVelocity.Y);
    }

    public void EndRunning()
    {
        IsRunning = false;
        if (!IsJumping)
        {
            CurrentVelocity = new Vector2(0);
        }
    }

    public void BeginJumping()
    {
        IsJumping = true;
        CurrentVelocity = new Vector2(CurrentVelocity.X, -500);
    }

    public void EndJumping(Vector2 newPosition)
    {
        IsJumping = false;
        CurrentVelocity = new Vector2(CurrentVelocity.X, 0);
        CurrentPosition = newPosition;
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

    public void BoundaryCollision(double timeOfCollision)
    {
        CurrentVelocity = new Vector2(CurrentVelocity.X, 0);

        CurrentPosition = PreviousPosition + (PreviousVelocity * (float)timeOfCollision);
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

        if (CurrentVelocity.X < 0)
        {
            backwards = true;
        }
        else
        {
            backwards = false;
        }

        base.Draw(_spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        _inUseAnimation.Update(gameTime);
        base.Update(gameTime);
    }

    #endregion


}
