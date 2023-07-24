using Engine.GameObjects;
namespace GameClasses.GeneralClasses;

public static class Sorting
{
    /// <summary>
    /// Sorts a GameObject array by its DrawOrder
    /// </summary>
    /// <param name="unsorted">The unsorted list</param>
    /// <param name="reverse">Reverses the list if true</param>
    /// <returns>The sorted array</returns>
    public static GameObject[] SortByDrawOrder(GameObject[] unsorted, bool reverse=true)
    {
        GameObject[] sorted;
        // Bubble sort
        int n = unsorted.Length;
        bool swapped = true;
        while(swapped)
        {
            swapped = false;
            for (int j = 1; j < n; j++)
            {
                if (unsorted[j].DrawOrder < unsorted[j-1].DrawOrder)
                {
                    var temp = unsorted[j-1];
                    unsorted[j-1] = unsorted[j];
                    unsorted[j] = temp;
                    swapped = true;
                }
            }
            n--;
        }

        if (reverse)
        {
            return unsorted.Reverse().ToArray();
        }
        else
        {
            return unsorted;
        }
    }
}
