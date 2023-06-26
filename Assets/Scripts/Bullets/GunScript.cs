using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    Bullet bullet_prefab;
    


    public void Fire(float BulletSpeed, int BulletIndex)
    {

        bullet_prefab = BulletPool.Get();

        bullet_prefab.transform.position = gameObject.transform.position;
        bullet_prefab.transform.right = gameObject.transform.up;

        bullet_prefab.FireBullet(BulletSpeed,BulletIndex);

        bullet_prefab.tag = gameObject.tag;
    }
}