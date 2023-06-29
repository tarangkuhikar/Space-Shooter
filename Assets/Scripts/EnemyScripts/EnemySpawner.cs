using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemyBehaviour[] _enemy;

    [SerializeField]
    float _spawnSpeed;

    [SerializeField]
    Grid _grid;

    [SerializeField]
    float _radius;

    [SerializeField]
    int _verPos;

    [SerializeField]
    List<Vector3> PathPoints;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());

    }

    IEnumerator SpawnEnemies()
    {
        for (int i = -3; i < 3; i++)
        {
            EnemyBehaviour temp = Instantiate(_enemy[0], gameObject.transform.position, Quaternion.identity);
            PathPoints.Add(_grid.CellToWorld(new Vector3Int(2 * i, _verPos, 0)));

            temp.SetPath(PathPoints.ToArray());

            PathPoints.RemoveAt(PathPoints.Count - 1);
            yield return new WaitForSecondsRealtime(_spawnSpeed);

        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
