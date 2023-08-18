using Engine.Sprites;
using Microsoft.Xna.Framework;

namespace GameClasses.GeneralClasses;

public static class Collisions
{
    public static bool RectanglesColliding(Rectangle r1, Rectangle r2)
    {
        if (r1.Bottom >= r2.Top && r1.Top <= r2.Bottom)
        {
            if (r1.Right >= r2.Left && r1.Left <= r2.Right)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Root finding algorithm to determine the time at which particles are just colliding
    /// </summary>
    /// <param name="p1">Particle 1</param>
    /// <param name="p2">Particle 2</param>
    /// <param name="gameTime">GameTime object</param>
    /// <returns>The minimum time of collision</returns>
    public static double TimeOfCollision(Sprite p1, Sprite p2, GameTime gameTime)
    {
        Sprite a1 = p1;
        Sprite a2 = p2;
        double a = Math.Pow(a2.PreviousVelocity.Y + a2.PreviousVelocity.Y - (a1.PreviousVelocity.Y + a1.PreviousVelocity.X), 2);
        double b = 2 * (a2.PreviousPosition.X + a2.PreviousPosition.Y - (a1.PreviousPosition.X + a1.PreviousPosition.Y)) * (a2.PreviousVelocity.Y + a2.PreviousVelocity.Y - (a1.PreviousVelocity.Y + a1.PreviousVelocity.X));
        double c = Math.Pow(a2.PreviousPosition.X + a2.PreviousPosition.Y - (a1.PreviousPosition.X + a1.PreviousPosition.Y), 2) - Math.Pow(a1.YRadius + a2.YRadius, 2);

        double discriminant = Math.Pow(b, 2) - (4 * a * c);
        if (discriminant >= 0) // if there are roots
        {
            double[] roots = CalcRoots(a, b, c).Where(c => c >= 0).ToArray(); // find all positive roots
            if (roots.Length > 0)
            {
                return roots.Min();
            }
        }
        return gameTime.ElapsedGameTime.TotalSeconds;
    }

    /// <summary>
    /// Finds the roots of an equation in the form y = ax^2 + bx + c
    /// </summary>
    /// <param name="a">Coefficient of x^2</param>
    /// <param name="b">Coefficient of x</param>
    /// <param name="c">Constant</param>
    /// <returns>Array containing all roots</returns>
    private static double[] CalcRoots(double a, double b, double c)
    {
        return new double[]
        {
            (-b + Math.Sqrt(Math.Pow(b,2) - (4*a*c))) / 2*a,
            (-b - Math.Sqrt(Math.Pow(b,2) - (4*a*c))) / 2*a,
        };
    }

    /// <summary>
    /// Finds the smallest positive root of an equation
    /// </summary>
    /// <returns></returns>
    public static double SolveQuadratic(double a, double b, double c)
    {
        double[] roots = CalcRoots(a, b, c).Where(z => z >= 0).ToArray(); // find all positive roots
        if (roots.Length > 0)
        {
            return roots.Min();
        }
        return -1;
    }
}