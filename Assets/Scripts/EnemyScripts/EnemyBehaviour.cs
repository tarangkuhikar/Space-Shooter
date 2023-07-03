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

    Sequence s;
    /// <summary>
    /// Sets the path of the enemy while making it look straight ahead.
    /// </summary>
    /// <param name="setPath">The points through which the gameObject goes.</param>
    public void SetPath(Vector3[] setPath)
    {

        _path = setPath;
        s = DOTween.Sequence();
        s.Append(transform.DOPath(_path, 2, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.Linear).SetLookAt(0.02f, null, -gameObject.transform.right));
        s.Append(transform.DORotate(Vector3.up, 1).SetEase(Ease.InOutExpo));

    }
    /// <summary>
    /// Fires a bullet in the player's direction.
    /// </summary>
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
            _explosion = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            _explosion.Play();
            EnemyKilled?.Invoke();
            ScoreScript.ScoreChanged(_enemyData.Experience);
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        s.Kill(false);
    }
}