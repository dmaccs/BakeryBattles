using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodFight;
public static partial class MapGenerator : object
{
    private const int MAP_HEIGHT = 10;
    private const int MAP_WIDTH = 7;

    private const int MIN_ROW_COUNT = 3;
    public static List<List<int>> GenerateMap()
    {
        List<List<int>> map = new List<List<int>>(MAP_HEIGHT);
        for (int i = 0; i < MAP_HEIGHT; i++)
        {
            map.Add(GenerateRow(i));
        }
        return map;
    }

    public static List<int> GenerateRow(int level)
    {
        Random random = new Random((int)GameState.Instance.GetSeed());
        int totalRowCount = MIN_ROW_COUNT + random.Next(0, 2);
        List<int> possiblePositions = new List<int>(MAP_WIDTH);
        List<int> currRow = Enumerable.Repeat(0,MAP_WIDTH).ToList();
        for (int i = 0; i < MAP_WIDTH; i++)
        {
            possiblePositions.Add(i);
        }

        // Shuffle first NUM_CHOICES elements
        for (int k = 0; k < totalRowCount; k++)
        {
            int j = random.Next(k, possiblePositions.Count);
            (possiblePositions[k], possiblePositions[j]) = (possiblePositions[j], possiblePositions[k]);
        }

        for(int l = 0; l < totalRowCount; l++)
        {
            currRow[possiblePositions[l]] = 1;
        }
        GD.Print(currRow.ToString());
        return currRow;
    }
}
