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
    public static int level=1;
    [SerializeField]
    TMPro.TMP_Text _helperText;
    

    public void Awake()
    {
        StartCoroutine(GameStarted());
    }

    IEnumerator GameStarted()
    {
        _explosionAudio = gameObject.GetComponent<AudioSource>();
        Player = _player;
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;

        _helperText.text = "";
        yield return new WaitForSeconds(1);

        yield return StartCoroutine(ShowHelperText("Use the A,D or left arrow and right arrow key to move."));

        yield return new WaitUntil(() => Input.GetButton("Horizontal"));
        
        yield return new WaitForSeconds(0.5f);


        yield return StartCoroutine(ShowHelperText("Use the F or Space key to shoot."));
        yield return new WaitUntil(() => Input.GetButton("Fire1"));


        yield return new WaitForSeconds(0.5f);
        _helperText.text = "";
        yield return new WaitForSeconds(1);
        _enemySpawner.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnAsteroids());
    }



    IEnumerator ShowHelperText(string TextToShow)
    {

        _helperText.maxVisibleCharacters = 0;
        int length = TextToShow.Length;
        _helperText.text = TextToShow;
        for (int i = 0; i < length; i++)
        {
            _helperText.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.02f);
        }

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
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    private void OnDisable()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;

    }
}