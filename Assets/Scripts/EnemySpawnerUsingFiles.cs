using UnityEngine;
using System.Collections;

public static class EnemySpawnerUsingFiles

{
    static TextAsset[] LevelData;
    public static string SpawnEnemies(int Level)
    {
        LevelData = Resources.LoadAll<TextAsset>("Levels");
        return LevelData[Level].text;
    }
}
