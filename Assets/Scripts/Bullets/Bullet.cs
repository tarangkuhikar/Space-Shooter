using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer _bulletImage;
    float _bulletSpeed;
    int _bulletIndex = 5;

    // Update is called once per frame
    private void OnEnable()
    {
        _bulletImage = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        gameObject.transform.position += _bulletSpeed * Time.deltaTime * gameObject.transform.right;
    }

    public void FireBullet(float bulletSpeed, int bulletindex)
    {
        _bulletSpeed = bulletSpeed;
        _bulletIndex = bulletindex;
        StartCoroutine(BulletAnimation());
    }

    private IEnumerator BulletAnimation()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                _bulletImage.sprite = BulletSprite.GetSprite(_bulletIndex * 4 + i);
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
