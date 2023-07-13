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
    Vector3[] _spawnPoints;

    [SerializeField]
    EnemyBehaviour[] _enemyPrefab;


    [SerializeField]
    float fireChance = 0.3f;

    [SerializeField]
    float _delayBetweenDives = 5f;

    [SerializeField]
    Grid _grid;

    [SerializeField]
    BossScript[] _Boss;

    static int _enemyActive = 0;

    List<EnemyBehaviour> _enemyList;

    List<Vector3> PathPoints;
    int _enemyWaveSpawned = 0;
    public void Start()
    {
        _enemyActive = 0;

        EnemyBehaviour.EnemyKilled += EnemyKilled;
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;

        _enemyList = new List<EnemyBehaviour>();
        PathPoints = new List<Vector3>();

        //StartCoroutine(SpawnEnemies());
        Instantiate(_Boss[GameManager.level - 1], _spawnPoints[0], Quaternion.identity).StartBossFight();
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }
    IEnumerator SpawnEnemies()
    {
        _enemyWaveSpawned += 1;
        for (int j = 0; j < _spawnPoints.Length; j++)
        {
            for (int i = 0; i < _waveSize; i++)
            {

                _enemyList.Add(Instantiate(_enemyPrefab[0], _spawnPoints[j], Quaternion.identity));
                _enemyActive += 1;
                PathPoints.Add(_grid.CellToWorld(new Vector3Int(-2 * i + _waveSize + j % 2, -j, 0)));

                _enemyList[_waveSize * j + i].SetPath(PathPoints.ToArray(), 2);

                PathPoints.Clear();
                yield return new WaitForSeconds(_delayBetweenEnemies);
            }
            yield return new WaitForSeconds(_delayBetweenWaves);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(DiveEnemies());

    }


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
                    _enemyList[j * _waveSize + i].SetPath(new Vector3[] { x - 2 * Vector3.down - 1 * Vector3.right, x - 2 * Vector3.down - 1 * Vector3.left, _grid.CellToWorld(new Vector3Int(-2 * i + _waveSize + j % 2, -j, 0)) }, 2);

                    if (Random.value < fireChance)
                    {
                        StartCoroutine(FireBulletsRandomly(j * _waveSize + i));
                    }
                    yield return new WaitForSeconds(3 * _delayBetweenEnemies);
                }
            }
            yield return new WaitForSeconds(_delayBetweenDives);

        }
    }

    IEnumerator FireBulletsRandomly(int enemyIndex)
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
        if (_enemyList[enemyIndex] != null)
        {
            _enemyList[enemyIndex].FirePattern();
            _enemyList[enemyIndex].FirePattern();
        }
    }

    IEnumerator SpawnEnemies2()
    {
        _enemyWaveSpawned += 1;
        float x = Random.Range(-1f, 1f);
        int y = Random.Range(0, 15);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < _waveSize; i++)
        {
            _enemyList.Add(Instantiate(_enemyPrefab[1], _spawnPoints[1] + 3 * Vector3.up, Quaternion.identity));
            _enemyActive += 1;
            _enemyList[i].SetPath(new Vector3[] { _spawnPoints[1] + 3 * Vector3.up, _spawnPoints[1] + y * Vector3.right + (x + 3) * Vector3.up, _spawnPoints[1] + (y + 3 * Mathf.Abs(x)) * Vector3.right - (x - 3) * Vector3.up, 10 * Vector3.right }, 7);
            yield return new WaitForSeconds(0.1f);
            if (Random.value < fireChance)
            {
                StartCoroutine(FireBulletsRandomly(i));
            }
            yield return new WaitForSeconds(7 * _delayBetweenEnemies);
        }

        yield return new WaitForSeconds(8);
        foreach (EnemyBehaviour e in _enemyList)
        {
            if (e != null)
            {
                Destroy(e.gameObject);
            }
        }
        _enemyActive = 1;
        EnemyKilled();
    }
    void EnemyKilled()
    {

        _enemyActive -= 1;

        if (_enemyActive == 0)
        {
            fireChance += 0.2f;
            if (_delayBetweenDives >= 1)
            {
                _delayBetweenDives -= 0.5f;
            }
            StopAllCoroutines();
            _enemyList.Clear();

            if (_enemyWaveSpawned == 2)
            {
                StartCoroutine(SpawnEnemies2());
            }
            else if (_enemyWaveSpawned == 5)
            {
                Instantiate(_Boss[GameManager.level - 1], _spawnPoints[0], Quaternion.identity).StartBossFight();
            }
            else
            {
                StartCoroutine(SpawnEnemies());
            }
        }

    }

    private void OnDestroy()
    {
        EnemyBehaviour.EnemyKilled -= EnemyKilled;
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
    }
}
