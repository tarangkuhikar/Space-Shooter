using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    Bullet _bulletPrefab;

    public void Fire(float bulletSpeed, int bulletIndex)
    {
        _bulletPrefab = BulletPool.Get();

        _bulletPrefab.transform.position = gameObject.transform.position;
        _bulletPrefab.transform.right = gameObject.transform.up;

        _bulletPrefab.tag = gameObject.tag;

        _bulletPrefab.FireBullet(bulletSpeed, bulletIndex);

    }
}