using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.Sprites;
using GameClasses.GeneralClasses;


namespace GameClasses.Sprites;

public class Enemy : Sprite
{
    public Enemy(Color colour, Rectangle size, Vector2 velocity, Vector2 acceleration) : base(colour, size, velocity, acceleration, true)
    {

    }

    public override void Update(GameTime gameTime)
    {
        
    }
}