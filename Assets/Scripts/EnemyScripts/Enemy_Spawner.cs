using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField]
    EnemyBehaviour[] Enemy;

    [SerializeField]
    float spawn_speed;

    [SerializeField]
    Grid grid;

    [SerializeField]
    float radius;

    private void OnEnable()
    {
        StartCoroutine(Spawn_enemies());
    }

    IEnumerator Spawn_enemies()
    {
        for (int i = -3; i < 3; i++)
        {
            EnemyBehaviour temp = Instantiate(Enemy[0], gameObject.transform.position, Quaternion.identity);
            temp.SetPath(new Vector3[]{new Vector3(0,3,0), gameObject.transform.position + new Vector3(radius, -radius-1, 0),
            gameObject.transform.position + new Vector3(0, -2 * radius, 0),
            gameObject.transform.position + new Vector3(-radius, -radius-1, 0),grid.CellToWorld(new Vector3Int(2*i,-2,0))}, i);

            yield return new WaitForSecondsRealtime(spawn_speed);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn_enemies());
    }
}
