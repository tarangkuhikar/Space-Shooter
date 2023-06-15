using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    float spawn_speed;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Spawn_enemies());
    }

    IEnumerator Spawn_enemies()
    {
        while (true)
        {
            Instantiate(Enemy,new Vector3(Random.Range(-7,7),5), Quaternion.identity);
            yield return new WaitForSecondsRealtime(spawn_speed);
        }
    }
}
