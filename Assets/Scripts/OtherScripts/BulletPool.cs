using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    Bullet bullet_prefab;

    static ObjectPool<Bullet> bullet_pool;
    private void Awake()
    {
        bullet_pool = new ObjectPool<Bullet>(() => Instantiate(bullet_prefab),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(gameObject), true, 50, 100);;
    }

    public static Bullet Get()
    {
        return bullet_pool.Get();
    }

    public static void Release(Bullet bullet)
    {
        bullet_pool.Release(bullet);
    }
}
