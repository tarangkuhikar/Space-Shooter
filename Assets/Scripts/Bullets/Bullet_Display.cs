using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet_Display : MonoBehaviour
{
    SpriteRenderer bullet_image;
   
    static Sprite[] bullet_sprite = Resources.LoadAll<Sprite>("Bullets_asset_folder/Bullet_00");
    [SerializeField]
    float bullet_speed;
    static int bullet_index=5;

    private Action<Bullet_Display> _disableaction;
    void OnEnable()
    {
        bullet_image = gameObject.GetComponent<SpriteRenderer>();
        

        StartCoroutine(Bullet_animation(bullet_index));
       
    }

    private void Update()
    {
        gameObject.transform.position += new Vector3(0,bullet_speed);
        if (Input.GetAxis("Fire2") != 0)
        {
            Change_animation();
        }
    }

    // Update is called once per frame
    void Change_animation()
    {
        int x = UnityEngine.Random.Range(2,64);
        while(x==8||x==15||x==22){
            x = UnityEngine.Random.Range(2, 64);
        }
        bullet_index = 4*(x-1)+(5*(int)Mathf.Ceil(x/7));
    }

    IEnumerator Bullet_animation(int x)
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet_image.sprite = bullet_sprite[x + i];
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
    public void Init(Action<Bullet_Display> disable)
    {
        _disableaction = disable;
    }

    private void OnBecameInvisible()
    {
        StopAllCoroutines();
        _disableaction(this);
    }
}
