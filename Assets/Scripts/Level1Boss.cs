using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level1Boss : BossScript
{
    [SerializeField]
    EnemyData _enemyData;

    float health = 200;
    [SerializeField]
    GunScript[] _guns;

    [SerializeField]
    Transform _leftShield, _rightShield;

    [SerializeField]
    float _waittime;
    [SerializeField]
    private float _gunRotateSpeed;

    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(StartFiring()));
    }
    IEnumerator StartFiring()
    {
        _guns[0].transform.DORotate(new Vector3(0, 0, -45), _gunRotateSpeed).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetId(transform);
        _guns[1].transform.DORotate(new Vector3(0, 0, -45), _gunRotateSpeed).SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetId(transform);
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            _guns[0].Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            _guns[1].Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            yield return new WaitForSeconds(_waittime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            health -= 10;
            collision.gameObject.SetActive(false);
            if (health == 100)
            {
                _leftShield.DORotate(new Vector3(0, 0, 30), 3).SetEase(Ease.InOutSine).SetRelative();
                _rightShield.DORotate(new Vector3(0, 0, -30), 3).SetEase(Ease.InOutSine).SetRelative();
                _waittime = 0.5f;
            }

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
