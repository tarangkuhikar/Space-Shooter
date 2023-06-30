using DG.Tweening;
using UnityEngine;
using System.Collections;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData _enemyData;

    [SerializeField]
    GunScript[] _guns;

    Vector3[] _path;

    [SerializeField]
    ParticleSystem _explosion;
    private void Start()
    {
        PlayerBehaviour.PlayerDied += PlayerBehaviour_PlayerDied;
        
    }

    public void SetPath(Vector3[] setPath)
    {
        
        _path = setPath;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOPath(_path, 2, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.Linear).SetLookAt(0.02f, null, -gameObject.transform.right));
        s.Append(transform.DORotate(Vector3.up, 1).SetEase(Ease.InOutExpo));
        
    }

    private void PlayerBehaviour_PlayerDied()
    {
        StopAllCoroutines();
    }


    public void FirePattern()
    {

        float shoot = Random.value;
        if (shoot <= 0.5f)
        {
            foreach (GunScript gun in _guns)
            {
                gun.transform.up = GameManager.Player.position - gun.transform.position;
                gun.Fire(_enemyData.BulletSpeed, _enemyData.BulletIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _explosion = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            _explosion.Play();
            Destroy(gameObject);
            ScoreScript.ScoreChanged(_enemyData.Experience);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PlayerBehaviour.PlayerDied -= PlayerBehaviour_PlayerDied;
        transform.DOKill();
    }
}