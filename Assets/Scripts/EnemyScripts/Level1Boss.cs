using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level1Boss : BossScript
{
    [SerializeField]
    EnemyData _enemyData;

    float health = 100;
    [SerializeField]
    GunScript _guns;
    [SerializeField]
    private float _waittime;
    bool _isDamageable = false;
    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(StartFiring()));
    }

    IEnumerator StartFiring()
    {

        _guns.transform.DORotate(new Vector3(0, 0, 225), 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetId(transform);
        yield return new WaitForSeconds(0.5f);
        _isDamageable = true;
        while (true)
        {
            _guns.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            yield return new WaitForSeconds(_waittime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (_isDamageable)
            {

                health -= 10;
            }
            collision.gameObject.SetActive(false);
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
        StopAllCoroutines();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
