using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level5Boss : BossScript
{

    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(StartFiring()));
    }

    IEnumerator StartFiring()
    {
        while (true)
        {
            _guns[0].transform.up = GameManager.Player.position - _guns[0].transform.position;
            for (int i = 0; i < 5; i++)
            {
                _guns[0].Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(Random.Range(0f, 3f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _health -= 10;
            collision.gameObject.SetActive(false);
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
