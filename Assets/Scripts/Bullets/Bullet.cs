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

    public IEnumerator BulletAnimation()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = Bullet_Sprite.GetSprite(4 * (bullet_index - 1) + 5 * (int)(Mathf.Ceil(bullet_index / 7)) + 1 + i);

                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(BulletAnimation());
    }

    public void BulletIndexChanged(int i)
    {
        bullet_index = i;
    }

    public void OnDisable()
    {
        StopCoroutine(BulletAnimation());
        BulletPool.Release(this);

    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
