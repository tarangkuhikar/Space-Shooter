using DG.Tweening;
using UnityEngine;
using System;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData _enemyData;

    [SerializeField]
    GunScript[] _guns;

    Vector3[] _path;

    [SerializeField]
    ParticleSystem _explosion;
    public static event Action EnemyKilled;

    Sequence _enemyPath;
    
    public void SetPath(Vector3[] setPath,float pathSpeed)
    {
        _enemyPath.Kill();
        _path = setPath;
        _enemyPath = DOTween.Sequence();
        _enemyPath.SetId(transform);
        _enemyPath.Append(transform.DOPath(_path, pathSpeed, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.Linear).SetLookAt(0.02f, null, -gameObject.transform.right));
        _enemyPath.Append(transform.DORotate(Vector3.up, 1).SetEase(Ease.InOutExpo));
        _enemyPath.onComplete += IdleMotion; 

    }

    private void IdleMotion()
    {
        _enemyPath = DOTween.Sequence();
        _enemyPath.SetId(transform);
        _enemyPath.Append(transform.DOLocalMoveX(transform.position.x+0.5f,2));
        _enemyPath.SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void FirePattern()
    {

        foreach (GunScript gun in _guns)
        {
            gun.transform.up = GameManager.Player.position - gun.transform.position;
            gun.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            GameManager.ExplosionAudio();
            _explosion = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            _explosion.Play();
            EnemyKilled?.Invoke();
            ScoreScript.ScoreChanged(_enemyData.Experience);
            collision.gameObject.SetActive(false);
            transform.DOKill();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}