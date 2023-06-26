using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer bullet_image;
    float BulletSpeed;
    int bullet_index = 5;

    // Update is called once per frame
    private void OnEnable()
    {
        bullet_image = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        gameObject.transform.position += BulletSpeed * Time.deltaTime * gameObject.transform.right;
    }

    public void FireBullet(float bulletSpeed, int bulletindex)
    {
        BulletSpeed = bulletSpeed;
        bullet_index = bulletindex;
        StartCoroutine(BulletAnimation());
    }

    private IEnumerator BulletAnimation()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = BulletSprite.GetSprite(bullet_index * 4 + i);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    private void OnDisable()
    {
        BulletPool.Release(this);
        StopAllCoroutines();
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
