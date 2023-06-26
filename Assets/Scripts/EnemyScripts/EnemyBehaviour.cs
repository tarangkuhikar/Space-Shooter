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
    float shoottime;
    private void start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.DOPath(path, 3+0.5f*shoottime, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.green).SetEase(Ease.Linear);
        StartCoroutine(FirePattern());
    }

    public void SetPath(Vector3[] setpath,int index)
    {
        shoottime = index;
        path = setpath;
        start();
    }
    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }

    IEnumerator FirePattern()
    {
        float shoot = Random.value;
        while (true)
        {
            if (shoot < 0.25)
            {
                foreach (GunScript gun in guns)
                {
                    gun.transform.up = player.transform.position - gun.transform.position;
                    gun.Fire(EnemyData.BulletSpeed, EnemyData.BulletIndex);
                }
                yield return new WaitForSecondsRealtime(3);
            }
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
        DOTween.KillAll();
    }
}
