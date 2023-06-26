using System.Collections;
using UnityEngine;

public class GameManager :MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }
    static string leveldata;
    [SerializeField]
    GameObject[] Enemy;
    private void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        leveldata = EnemySpawnerUsingFiles.SpawnEnemies(0);
        StartCoroutine(SpawnEnemies());
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies()
    {
        
        foreach(char i in leveldata)
        {
            Instantiate(Enemy[i-'0'],new Vector2(Random.Range(-6,6),5),Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
    }

    public void LevelComplete()
    {
        Debug.Log("game Over");
    }

    private void OnDisable()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
    }
}