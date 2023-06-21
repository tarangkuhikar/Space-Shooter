using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer bullet_image;
    [SerializeField]
    float bullet_speed;
    int bullet_index = 5;
    // Update is called once per frame
    void OnEnable()
    {
        bullet_image = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        gameObject.transform.position += bullet_speed * Time.deltaTime * gameObject.transform.right;
    }

    public  void StartAnimation()
    {
        StartCoroutine(BulletAnimation());
    }

    public IEnumerator BulletAnimation()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = Bullet_Sprite.GetSprite(bullet_index*4+i);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void bulletspeed(float bulletSpeed)
    {
        bullet_speed = bulletSpeed;
    }

    public void BulletIndexChanged(int i)
    {
        bullet_index = i;
    }

    public void OnDisable()
    {
        BulletPool.Release(this);
        StopAllCoroutines();
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
