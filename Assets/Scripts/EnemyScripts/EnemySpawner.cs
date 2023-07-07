using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int _waveSize;

    [SerializeField]
    float _delayBetweenEnemies;

    [SerializeField]
    float _delayBetweenWaves;

    [SerializeField]
    Grid _grid;

    [SerializeField]
    Vector3[] _spawnPoints;

    [SerializeField]
    EnemyBehaviour _enemyPrefab;

    public static List<EnemyBehaviour> _enemyList;

    [SerializeField]
    private float _fireChance;
    [SerializeField]
    private float _delayBetweenDives;

    public void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        _enemyList = new List<EnemyBehaviour>();
        StartCoroutine(SpawnEnemies());
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }
    IEnumerator SpawnEnemies()
    {
        for (int j = 0; j < _spawnPoints.Length; j++)
        {
            for (int i = 0; i < _waveSize; i++)
            {
                _enemyList.Add(Instantiate(_enemyPrefab, _spawnPoints[j], Quaternion.identity));
                _enemyList[j * _waveSize + i].MoveGridPos(_grid.CellToWorld(new Vector3Int(-2 * i + _waveSize + j % 2, -j, 0)));
                yield return new WaitForSeconds(_delayBetweenEnemies);
            }
            yield return new WaitForSeconds(_delayBetweenWaves);
        }
        StartCoroutine(DiveEnemies());
    }

    IEnumerator DiveEnemies()
    {
        for (int i = 0; i < _waveSize; i++)
        {
            int x = Random.Range(0, _enemyList.Count - 1);
            
            _enemyList[x].DiveTowardsPlayer();
            if (Random.value < _fireChance)
            {
                _enemyList[x].Invoke("FirePattern", Random.Range(0.5f,2f));
            }
            yield return new WaitForSeconds(_delayBetweenDives);
        }
    }
    private void OnDestroy()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
    }
}
