using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    Bullet _bulletPrefab;

    static ObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(gameObject), true, 50, 100); 
    }

    public static Bullet Get()
    {
        return _bulletPool.Get();
    }

    public static void Release(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
}
