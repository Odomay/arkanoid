using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevlCombinationsStorage 
{
    public static Dictionary<int, LevelCombination> Combinations = new Dictionary<int, LevelCombination>();
}

public struct LevelCombination
{
    public int SpawnIndex;

    public LevelCombination(int spawn)
    {
        SpawnIndex = spawn;
    }
}
