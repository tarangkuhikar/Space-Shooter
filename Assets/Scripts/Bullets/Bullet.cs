using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer bullet_image;
    float BulletSpeed;
    int bullet_index = 5;

    // Update is called once per frame
    void OnEnable()
    {
        bullet_image = gameObject.GetComponent<SpriteRenderer>();
    }

    public  void StartAnimation(int bulletindex)
    {
        bullet_index = bulletindex;
        StartCoroutine(BulletAnimation());
    }

    private void Update()
    {
        gameObject.transform.position += BulletSpeed*Time.deltaTime*gameObject.transform.up;
    }

    public void Setbulletspeed(float bulletSpeed)
    {
        BulletSpeed = bulletSpeed;
    }

    private IEnumerator BulletAnimation()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = BulletSprite.GetSprite(bullet_index*4+i);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    public void OnDisable()
    {
        BulletPool.Release(this);
        StopAllCoroutines();
    }

    public void OnApplicationQuit()
    {
        Destroy(gameObject);
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
