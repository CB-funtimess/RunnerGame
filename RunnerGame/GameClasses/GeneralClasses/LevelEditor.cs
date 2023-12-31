using System.Diagnostics;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Engine.GameObjects;
using Engine.Sprites;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using System.Globalization;


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
        "Levels/Level1.txt",
        "Levels/Level2.txt",
        "Levels/Level3.txt",
        "Levels/Level4.txt",
        "Levels/Level5.txt"
    };
    public Rectangle AvailableRectangle { get; private set; }
    #endregion

    #region Properties
    #endregion

    #region Methods
    public LevelEditor(Rectangle window, Rectangle topBorder, Rectangle bottomBorder, Rectangle rightBorder, Rectangle leftBorder)
    {
        Point topLeft, bottomRight, size;
        if (rightBorder != default && leftBorder != default)
        {
            topLeft = new Point(leftBorder.Right, topBorder.Bottom);
            bottomRight = new Point(rightBorder.Left, bottomBorder.Top);
            size = new Point(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }
        else
        {
            topLeft = new Point(window.Left, topBorder.Bottom);
            bottomRight = new Point(window.Right, bottomBorder.Top);
            size = new Point(window.Width, window.Height - (topBorder.Height + bottomBorder.Height));
        }
        AvailableRectangle = new Rectangle(topLeft, size);


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
        float xScale = AvailableRectangle.Width / 128;
        float yScale = AvailableRectangle.Height / 56;
        Point topLeft = new Point((int)(AvailableRectangle.Left + (xScale * start.X)), (int)(AvailableRectangle.Top + (yScale * start.Y)));
        Point size = new Point((int)((end.X - start.X + 1) * xScale), (int)((end.Y - start.Y + 1) * yScale));
        return new Rectangle(new Point(topLeft.X, topLeft.Y), size);
    }

    public Rectangle[] ReadLevelFile()
    {
        List<Rectangle> rectangles = new List<Rectangle>();
        using (var sr = new StreamReader(currentLevelFile))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Length > 2)
                {
                    // Process to get all 4 points
                    // Start:0,0,End:0,0
                    string[] numbers = line.Split(',');
                    //"Start:0", "0", "End:0", "0"
                    // Using regex for sanitisation
                    string pattern = "(Start:|End:)";
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        numbers[i] = Regex.Replace(numbers[i], pattern, String.Empty);
                    }
                    Point startPoint = new Point(Convert.ToInt32(numbers[0]), Convert.ToInt32(numbers[1])), endPoint = new Point(Convert.ToInt32(numbers[2]), Convert.ToInt32(numbers[3]));
                    Rectangle temp = DecipherCoordinates(startPoint, endPoint);
                    rectangles.Add(temp);
                }
            }
        }
        return rectangles.ToArray();
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
