using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level1Boss : BossScript
{
    [SerializeField]
    private float _waittime;
    bool _isDamageable = false;
    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(StartFiring()));
    }

    IEnumerator StartFiring()
    {

        _guns[0].transform.DORotate(new Vector3(0, 0, 225), 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetId(transform);
        yield return new WaitForSeconds(0.5f);
        _isDamageable = true;
        while (true)
        {
            _guns[0].Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            yield return new WaitForSeconds(_waittime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (_isDamageable)
            {

               _health -= 10;
            }
            collision.gameObject.SetActive(false);
        }

        if (_health == 0)
        {
            Destroy(gameObject);
        }
    }

}
