using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    EnemySpawner _enemySpawner;

    [SerializeField]
    AsteroidBehaviour _asteroid;

    [SerializeField]
    float _astereoidSpawnChance;
    public static Transform Player;
    [SerializeField]
    Transform _player;
    float _asteroidSpawnPoint;
    IEnumerator Start()
    {
        Player = _player;
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;

        yield return new WaitForSeconds(3);
        _enemySpawner.gameObject.SetActive(true);

        StartCoroutine(SpawnAsteroids());

    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            _asteroidSpawnPoint = Random.Range(0f, Mathf.PI);

            if (Random.value < _astereoidSpawnChance)
            {
                Instantiate(_asteroid, new Vector3(11 * Mathf.Cos(_asteroidSpawnPoint), 11 * Mathf.Sin(_asteroidSpawnPoint), 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(5);
        }
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;

    }
}