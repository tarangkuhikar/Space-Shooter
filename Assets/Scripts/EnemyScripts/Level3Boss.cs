using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level3Boss : BossScript
{
    [SerializeField]
    GunScript _guns;
    [SerializeField]
    EnemyData _enemyData;
    [SerializeField]
    float _waitTime;
    float health = 150;
    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).onComplete += StartFiring;
    }

    public void StartFiring()
    {
        transform.DOMoveX(2, 2).SetEase(Ease.InOutSine).OnComplete(
            () => transform.DOMoveX(-4, 4).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            _guns.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            health -= 10;
            collision.gameObject.SetActive(false);
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
