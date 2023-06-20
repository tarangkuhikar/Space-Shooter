using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    Bullet bullet_prefab;
    int bullet_index;

    public void Fire()
    {
        bullet_prefab = BulletPool.Get();
        bullet_prefab.transform.position = gameObject.transform.position;
        bullet_prefab.transform.right = gameObject.transform.up;

        bullet_prefab.BulletIndexChanged(bullet_index);
        bullet_prefab.tag = gameObject.tag;
        bullet_prefab.StartAnimation();
    }

    public void Changebullet(int i)
    {
        bullet_index = i;
    }
}