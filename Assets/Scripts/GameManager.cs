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

    static AudioSource _explosionAudio;

    [SerializeField]
    TMPro.TMP_Text _helperText;

    IEnumerator Start()
    {
        _explosionAudio = gameObject.GetComponent<AudioSource>();
        Player = _player;

        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;

        yield return new WaitForSeconds(1);

        _enemySpawner.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
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
    public static void ExplosionAudio()
    {
        _explosionAudio.Play();
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private void OnDisable()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;

    }
}