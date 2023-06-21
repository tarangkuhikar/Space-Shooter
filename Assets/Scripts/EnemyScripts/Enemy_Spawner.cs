using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] Enemy;
    [SerializeField]
    float spawn_speed;
    Coroutine spawn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        spawn=StartCoroutine(Spawn_enemies());
    }

    IEnumerator Spawn_enemies()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(Enemy[0], new Vector3(Random.Range(-7, 7), 5), Quaternion.identity);
            yield return new WaitForSecondsRealtime(spawn_speed);
        }
        Instantiate(Enemy[1], new Vector3(0, 5), Quaternion.identity);
        StopCoroutine(spawn);
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn_enemies());
    }
}
