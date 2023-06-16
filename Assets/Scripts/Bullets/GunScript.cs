using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class GunScript : MonoBehaviour
{   
    [SerializeField]
    Bullet bullet_prefab;

    ObjectPool<Bullet> bullet_pool;
    private void Awake()
    {
        bullet_pool = new ObjectPool<Bullet>(()=>Instantiate(bullet_prefab,gameObject.transform.position,bullet_prefab.transform.rotation),
            bullet => {
                bullet.gameObject.SetActive(true);
                bullet.gameObject.transform.position = gameObject.transform.position;
            },
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(gameObject), true, 10, 20);
    }

    public void Fire()
    {
        bullet_pool.Get();
    }

    public void Changebullet(int i)
    {
        bullet_prefab.BulletIndexChanged(i);
    }
    public void Release(Bullet bullet)
    {
        bullet_pool.Release(bullet);
    }
}
