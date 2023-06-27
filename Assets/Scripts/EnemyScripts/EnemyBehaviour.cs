using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData EnemyData;

    [SerializeField]
    GunScript[] guns;

    Transform player;
    Vector3[] path;
    float movetime;

    private void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(FirePattern());
    }

    public void SetPath(Vector3[] setpath, int i)
    {
        path = setpath;
        movetime = i;
        transform.DOPath(path, 5, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.green).SetEase(Ease.Linear);
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }

    IEnumerator FirePattern()
    {

        while (true)
        {
            float shoot = Random.value;
            if (shoot <= 0.50f)
            {
                foreach (GunScript gun in guns)
                {
                    gun.transform.up = player.transform.position - gun.transform.position;
                    gun.Fire(EnemyData.BulletSpeed, EnemyData.BulletIndex);
                }
            }
            yield return new WaitForSecondsRealtime(8);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
            ScoreScript.ScoreChanged(EnemyData.Experience);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
        transform.DOKill();
    }
}
