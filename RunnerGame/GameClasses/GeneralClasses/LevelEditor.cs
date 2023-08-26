using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.Sprites;
using Microsoft.Xna.Framework.Input.Touch;
using System.Security.Cryptography.X509Certificates;


namespace GameClasses.GeneralClasses;

public class LevelEditor
{
    // Reads in a .txt file for the occurrences on the level
        // Each line contains the start and end points of a platform
        // i.e. 0,0 1,0 represents a 32x16 rectangle in the top left corner
    // Playable area is split into a 128x56 grid, so points addressable up to 127x55 since first coord is 0,0
    #region Fields
    private string currentLevelFile;
    private string[] allLevels = new string[]
    {
        "Level1.txt",
        "Level2.txt",
        "Level3.txt",
        "Level4.txt",
        "Level5.txt"
    };
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
        
        currentLevelFile = allLevels[0];
    }

    /// <summary>
    /// Finds the correct position for a set of coordinates from the level editor.
    /// </summary>
    /// <param name="start">The start coordinate in the grid.</param>
    /// <param name="end">The end coordinate in the grid. The end coordinates must be greater than the start coordinates.</param>
    /// <returns></returns>
    private Rectangle DecipherCoordinates(Point start, Point end)
    {
        float xScale = availableRect.Width / 128;
        float yScale = availableRect.Height / 56;
        Point topLeft = new Point((int)(availableRect.Left + (xScale * start.X)), (int)(availableRect.Top + (yScale * start.Y)));
        Point size = new Point((int)((end.X - start.X + 1) * xScale), (int)((end.Y - start.Y + 1) * yScale));
        return new Rectangle(new Point(topLeft.X + (size.X / 2), topLeft.Y + (size.Y / 2)), size);
    }

    private Rectangle[] ReadTextFile()
    {
        List<Point> startPoints = new List<Point>(), endPoints = new List<Point>();
        using (var sr = new StreamReader(currentLevelFile))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Process to get all 4 points
                // 0,0, 0,0
                string[] chars = line.Split(',');
            }
        }
        return null;
    }

    public bool TriggerNextLevel()
    {
        int indexOfCurrent = Array.FindIndex(allLevels, row => row == currentLevelFile);
        string newLevelFile = allLevels.ElementAtOrDefault(indexOfCurrent + 1);
        if (newLevelFile == default)
        {
            return false;
        }
        return true;
    }

    #endregion
}
