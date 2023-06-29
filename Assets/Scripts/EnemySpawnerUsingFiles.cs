using UnityEngine;
public static class EnemySpawnerUsingFiles

{
    static TextAsset[] _levelData;
    public static string SpawnEnemies(int level)
    {
        _levelData = Resources.LoadAll<TextAsset>("Levels");
        return _levelData[level].text;
    }
}
