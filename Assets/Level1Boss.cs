using UnityEngine;
using System.Collections;

public class Level1Boss : MonoBehaviour
{
    [SerializeField]
    EnemyData _enemyData;

    float health = 200;
    [SerializeField]
    GunScript[] _guns;
    private void Start()
    {
        StartCoroutine(FirePattern());
    }

    IEnumerator FirePattern()
    {
        while(true)       
        {
            foreach (GunScript gun in _guns)
            {
                gun.transform.up = GameManager.Player.position - gun.transform.position;
                gun.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 10;
            collision.gameObject.SetActive(false);
        }

        if (health == 100)
        {

        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
