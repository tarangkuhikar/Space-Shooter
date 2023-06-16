using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    SpriteRenderer bullet_image;

    [SerializeField]
    float bullet_speed;
    [SerializeField]
    int bullet_index=5;

    // Update is called once per frame
    void OnEnable()
    {
        bullet_image = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Bullet_animation());
    }

    private void Update()
    {
        gameObject.transform.position += new Vector3(0, bullet_speed * Time.deltaTime);

    }



    public IEnumerator Bullet_animation()
    {
        int index = bullet_index;
        Debug.Log(index);
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = Bullet_Sprite.GetSprite(index + i);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }

    public void BulletIndexChanged(int i)
    {
        bullet_index = i;       
    }

    private void OnBecameInvisible()
    {
        StopAllCoroutines();
    }
}
