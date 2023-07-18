using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Boss : BossScript
{
    [SerializeField]
    EnemyBehaviour _underlingEnemy;
    int _activeEnemies = 0;
    List<EnemyBehaviour> _enemyList = new List<EnemyBehaviour>();
    bool _isDamageable = false;
    public override void StartBossFight()
    {
        EnemyBehaviour.EnemyKilled += EnemyBehaviour_EnemyKilled;
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(SpawnUnderlings()));
    }

    private void EnemyBehaviour_EnemyKilled()
    {
        _activeEnemies--;

        if (_activeEnemies == 0)
        {
            StopAllCoroutines();
            StartCoroutine(StartFiring());
            _enemyList.Clear();
        }
    }

    IEnumerator StartFiring()
    {
        yield return new WaitForSeconds(2);
        _isDamageable = true;
        int _prevHealth = _health;
        while (_health > _prevHealth - 45)
        {
            _guns[0].transform.up = (GameManager.Player.position - _guns[0].transform.position).normalized + UnityEngine.Random.Range(-0.5f, 0.5f) * Vector3.right;
            _guns[0].Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            yield return new WaitForSeconds(0.8f);
        }
        _isDamageable = false;
        StartCoroutine(SpawnUnderlings());
    }

    IEnumerator SpawnUnderlings()
    {
        for (int i = 0; i < 6; i++)
        {
            _activeEnemies++;
            _enemyList.Add(Instantiate(_underlingEnemy, new Vector3(0, 6, 0), Quaternion.identity));
            _enemyList[i].SetPath(new Vector3[] { new Vector3(2.5f * (i - 3), gameObject.transform.position.y - 2) }, 2);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(DiveEnemies());
    }

    IEnumerator DiveEnemies()
    {
        while (true)
        {
            int x = UnityEngine.Random.Range(0, _enemyList.Count);
            while (_enemyList[x] == null)
            {
                x = UnityEngine.Random.Range(0, _enemyList.Count);
                yield return null;
            }
            _enemyList[x].FirePattern();
            yield return new WaitForSeconds(1);
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
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
