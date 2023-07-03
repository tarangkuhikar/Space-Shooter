using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int _waveSize;

    [SerializeField]
    float _delayBetweenEnemiesS;
    [SerializeField]
    float _delayBetweenWavesS;
    [SerializeField]
    Grid _grid;

    [SerializeField]
    Vector3[] _spawnPoints;

    [SerializeField]
    EnemyBehaviour _enemyPrefab;

    static int _enemyActive = 0;

    List<EnemyBehaviour> _enemyList;

    List<Vector3> PathPoints;

    WaitForSeconds _delayBetweenEnemies;
    WaitForSeconds _delayBetweenWaves;
    [SerializeField]
    float fireChance = 0.3f;
    [SerializeField]
    float _delayBetweenDives = 5f;

    public void Start()
    {
        _enemyActive = 0;
        EnemyBehaviour.EnemyKilled += EnemyKilled;
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        _enemyList = new List<EnemyBehaviour>();
        PathPoints = new List<Vector3>();
        _delayBetweenEnemies = new WaitForSeconds(_delayBetweenEnemiesS);
        _delayBetweenWaves = new WaitForSeconds(_delayBetweenWavesS);
        StartCoroutine(SpawnEnemies());
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// [deprecated] Spawns Enemies at the spawnPoints in number of waveSize.
    /// </summary>
    IEnumerator SpawnEnemies()
    {
        for (int j = 0; j < _spawnPoints.Length; j++)
        {
            for (int i = 0; i < _waveSize; i++)
            {

                _enemyList.Add(Instantiate(_enemyPrefab, _spawnPoints[j], Quaternion.identity));
                _enemyActive += 1;
                PathPoints.Add(_grid.CellToWorld(new Vector3Int(-2 * i + _waveSize + j % 2, -j, 0)));

                _enemyList[_waveSize * j + i].SetPath(PathPoints.ToArray());

                PathPoints.Clear();
                yield return _delayBetweenEnemies;
            }
            yield return _delayBetweenWaves;
        }

        StartCoroutine(DiveEnemies());
    }
    /// <summary>
    /// Makes enemies dive towards the player.
    /// </summary>
    /// <returns></returns>
    IEnumerator DiveEnemies()
    {
        while (true)
        {
            int j = Random.Range(0, _spawnPoints.Length);
            Vector3 x = GameManager.Player.position - Random.Range(-1.5f, 1.5f) * Vector3.right;
            for (int i = 0; i < _waveSize; i++)
            {
                if (_enemyList[j * _waveSize + i] != null)
                {
                    _enemyList[j * _waveSize + i].SetPath(new Vector3[] { x - 2 * Vector3.down - 1 * Vector3.right, x - 2 * Vector3.down - 1 * Vector3.left, _grid.CellToWorld(new Vector3Int(-2 * i + _waveSize + j % 2, -j, 0)) });
                    yield return new WaitForSeconds(0.2f);

                    if (Random.value < fireChance && _enemyList[j * _waveSize + i] != null)
                    {
                        _enemyList[j * _waveSize + i].FirePattern();
                    }

                }
            }
            yield return new WaitForSeconds(_delayBetweenDives);

        }
    }

    void EnemyKilled()
    {
        _enemyActive -= 1;
        if (_enemyActive == 0)
        {
            fireChance += 0.2f;
            if (_delayBetweenDives >= 2)
            {
                _delayBetweenDives -= 0.5f;
            }

            StopAllCoroutines();
            _enemyList.Clear();
            StartCoroutine(SpawnEnemies());
        }
    }

    private void OnDestroy()
    {
        EnemyBehaviour.EnemyKilled -= EnemyKilled;
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
    }
}
