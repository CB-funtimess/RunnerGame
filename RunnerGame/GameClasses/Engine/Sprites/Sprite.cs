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
    protected Vector2 position;
    protected Point dimensions;
    protected bool _doesDamageToPlayer;
    #endregion

    #region Properties
    public Vector2 Velocity {get; protected set;}
    public Vector2 Acceleration {get; protected set;}

    public Vector2 TopLeftPoint => new Vector2(position.X - XRadius, position.Y - YRadius);
    public Rectangle ObjectRectangle => new Rectangle((int)TopLeftPoint.X, (int)TopLeftPoint.Y, dimensions.X, dimensions.Y);
    public int XRadius => dimensions.X / 2;
    public int YRadius => dimensions.Y / 2;
    #endregion

    #region Methods
    public Sprite(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration, bool doesDamageToPlayer)
    {
        Velocity = velocity;
        Acceleration = acceleration;
        _doesDamageToPlayer = doesDamageToPlayer;

        this.colour = colour;
        dimensions = size.Size;
        position = size.Center.ToVector2();
    }

    public void SetStandardAnimation(Animation standard)
    {
        standardAnimation = standard;
        _inUseAnimation = standardAnimation;
    }

    public virtual void Update(GameTime gameTime)
    {
        Velocity += Vector2.Multiply(Acceleration, (float)gameTime.ElapsedGameTime.TotalSeconds);
        position += Vector2.Multiply(Velocity, (float)gameTime.ElapsedGameTime.TotalSeconds);
    }

    public virtual void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.Draw(_inUseAnimation.SpriteSheet, ObjectRectangle, _inUseAnimation.CurrentFrame.SourceRectangle, colour);
    }
    #endregion
}