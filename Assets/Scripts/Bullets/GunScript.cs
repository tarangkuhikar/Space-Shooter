using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    Bullet bullet_prefab;
    int bullet_index;
    [SerializeField]
    float BulletSpeed=1;
    
    public void Fire()
    {
        bullet_prefab = BulletPool.Get();

        bullet_prefab.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        bullet_prefab.Setbulletspeed(BulletSpeed);

        bullet_prefab.tag = gameObject.tag;
        bullet_prefab.StartAnimation(bullet_index);
    }

    public void Changebullet(int i)
    {
        bullet_index = i;
    }

    public void SetSpeed(float bulletSpeed)
    {
        BulletSpeed = bulletSpeed;    
    }
}