using DG.Tweening;
using UnityEngine;
using System;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData _enemyData;

    [SerializeField]
    GunScript[] _guns;

    [SerializeField]
    ParticleSystem _explosion;
    public static event Action EnemyKilled;
    Sequence _enemyPath;
    Vector2 _gridPos;

    public void SetPath(Vector3[] setPath, float pathSpeed)
    {
        _enemyPath = DOTween.Sequence();
        _enemyPath.Append(transform.DOPath(setPath, pathSpeed, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.Linear).SetLookAt(0.02f, null, -gameObject.transform.right));
        _enemyPath.Append(transform.DORotate(Vector3.up, 1).SetEase(Ease.InOutExpo));

    }

    public void FirePattern()
    {

        foreach (GunScript gun in _guns)
        {
            gun.transform.up = GameManager.Player.position - gun.transform.position;
            gun.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
        }
    }

    public void MoveGridPos(Vector2 GridPos)
    {
        _gridPos = GridPos;
        SetPath(new Vector3[] { gameObject.transform.position,_gridPos},3);
    }

    public void DiveTowardsPlayer()
    {
        Vector3 x = GameManager.Player.position - UnityEngine.Random.Range(-1.5f, 1.5f) * Vector3.right;
        SetPath(new Vector3[] { x - 2 * Vector3.down - 1 * Vector3.right, x - 2 * Vector3.down - 1 * Vector3.left, _gridPos }, 2);
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
            _enemyPath.Kill();
            EnemySpawner._enemyList.Remove(this);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _enemyPath.Kill();
    }
}