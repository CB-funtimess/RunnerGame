using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.Sprites;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameClasses.GeneralClasses;

public class LevelEditor
{
    // Reads in a .txt file for the occurrences on the level
        // Each line contains the start and end points of a platform
        // i.e. 0,0 1,0 represents a 32x16 rectangle in the top left corner
    // Playable area is split into a 128x56 grid, so points addressable up to 127x55 since first coord is 0,0
    #region Fields
    private string file = "level.txt";
    private Rectangle availableRect;
    #endregion

    #region Properties
    #endregion

    #region Methods
    public LevelEditor(Rectangle window, Rectangle topBorder, Rectangle bottomBorder, Rectangle rightBorder, Rectangle leftBorder)
    {
        Point topLeft, bottomRight, size;
        if (rightBorder != null && leftBorder != null)
        {
            topLeft = new Point(leftBorder.Right, topBorder.Bottom);
            bottomRight = new Point(rightBorder.Left, bottomBorder.Top);
            size = new Point(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }
        else
        {
            topLeft = new Point(window.Left, topBorder.Bottom);
            bottomRight = new Point(window.Right, bottomBorder.Top);
            size = new Point(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }
        availableRect = new Rectangle(topLeft, size);
        
    }

    private Rectangle DecipherCoordinates(Point start, Point end)
    {
        float xScale = availableRect.Width / 128;
        float yScale = availableRect.Height / 56;
        
    }

    #endregion
}
