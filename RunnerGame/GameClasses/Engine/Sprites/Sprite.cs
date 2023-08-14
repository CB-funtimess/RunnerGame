using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
namespace Engine.Sprites;

public class Sprite
{
    #region Fields
    private Animation standardAnimation;
    protected Animation _inUseAnimation;

    protected Color colour;
    protected Point dimensions;
    protected bool _doesDamageToPlayer;
    protected bool backwards;
    #endregion

    #region Properties
    public int DrawOrder { get; set; }
    public Vector2 CurrentPosition {get; protected set;}
    public Vector2 PreviousPosition {get; protected set;}
    public Vector2 CurrentVelocity { get; protected set; }
    public Vector2 PreviousVelocity {get; protected set;}
    public Vector2 Acceleration { get; protected set; }
    public Vector2 TopLeftPoint => new Vector2(CurrentPosition.X - XRadius, CurrentPosition.Y - YRadius);
    public Rectangle ObjectRectangle => new Rectangle((int)TopLeftPoint.X, (int)TopLeftPoint.Y, dimensions.X, dimensions.Y);
    public int XRadius => dimensions.X / 2;
    public int YRadius => dimensions.Y / 2;
    #endregion

    #region Methods
    public Sprite(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration, bool doesDamageToPlayer)
    {
        CurrentVelocity = velocity;
        Acceleration = acceleration;
        _doesDamageToPlayer = doesDamageToPlayer;

        this.colour = colour;
        dimensions = size.Size;
        CurrentPosition = size.Center.ToVector2();
        backwards = false;
        DrawOrder = 5;
    }

    public void SetStandardAnimation(Animation standard)
    {
        standardAnimation = standard;
        _inUseAnimation = standardAnimation;
    }

    public virtual void Update(GameTime gameTime)
    {
        PreviousPosition = CurrentPosition;
        PreviousVelocity = CurrentVelocity;

        CurrentVelocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentPosition += CurrentVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(SpriteBatch _spriteBatch)
    {
        if (backwards)
        {
            _spriteBatch.Draw(_inUseAnimation.SpriteSheet, ObjectRectangle, _inUseAnimation.CurrentFrame.SourceRectangle, colour, 0, new Vector2(0), SpriteEffects.FlipHorizontally, 0);
        }
        else
        {
            _spriteBatch.Draw(_inUseAnimation.SpriteSheet, ObjectRectangle, _inUseAnimation.CurrentFrame.SourceRectangle, colour);
        }
    }
    #endregion
}