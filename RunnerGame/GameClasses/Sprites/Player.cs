using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Sprites;
using GameClasses.GeneralClasses;
using System.Security.Cryptography;


namespace GameClasses.Sprites;

public class Player : Sprite
{
    #region Fields
    private Animation runAnimation;
    private Animation idleAnimation;
    private Animation deathAnimation;
    public int speed;
    public int jumpSpeed;
    #endregion

    #region Properties
    public bool IsRunning { get; protected set; }
    public bool IsJumping { get; protected set; }
    public bool IsTouchingPlatform { get; set; }
    public int Hearts { get; private set; }
    public int Shields { get; private set; }
    #endregion

    #region Methods
    public Player(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration) : base(colour, size, velocity, acceleration, false)
    {
        Enabled = true;
        Hearts = 3;
        Shields = 3;
        IsRunning = false;
        IsJumping = false;
        IsTouchingPlatform = true;
    }

    public void BeginRunning(int direction)
    {
        if (!IsJumping)
        {
            IsRunning = true;
            CurrentVelocity = new Vector2(speed * direction, CurrentVelocity.Y);
            if (direction < 0)
            {
                backwards = true;
            }
            else
            {
                backwards = false;
            }
        }
    }

    public void EndRunning()
    {
        IsRunning = false;
        if (!IsJumping)
        {
            CurrentVelocity = new Vector2(0, CurrentVelocity.Y);
        }
    }

    public void BeginJumping()
    {
        IsJumping = true;
        CurrentVelocity = new Vector2(CurrentVelocity.X, jumpSpeed);
    }

    public void EndJumping(Vector2 newPosition)
    {
        IsJumping = false;
        CurrentVelocity = new Vector2(CurrentVelocity.X, 0);
        CurrentPosition = newPosition;
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        CurrentVelocity = newVelocity;
    }

    public void SetPosition(Vector2 newPosition)
    {
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
        if (Hearts == 0)
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
        if (IsJumping)
        {
            IsRunning = false;
        }
        if (IsRunning)
        {
            IsJumping = false;
        }
        if (CurrentVelocity.Y != 0)
        {
            IsJumping = true;
        }
        _inUseAnimation.Update(gameTime);
        IsTouchingPlatform = false;
        base.Update(gameTime);
    }

    #endregion
}