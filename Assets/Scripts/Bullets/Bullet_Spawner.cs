using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Spawner : MonoBehaviour
{
    [SerializeField]
    Bullet_Display bullet_prefab;
    ObjectPool<Bullet_Display> bullet_pool;
    // Update is called once per frame

    private void Start()
    {
        bullet_pool = new ObjectPool<Bullet_Display>(
            () => Instantiate(bullet_prefab, gameObject.transform.position, Quaternion.Euler(0, 0, 90)),
            bullet => { 
                bullet.gameObject.SetActive(true); 
                bullet.gameObject.transform.position = gameObject.transform.position; 
            },
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(gameObject), true, 10, 20);
    }
    void Update()
    {   
        if (Input.GetButtonDown("Fire1"))
        {
           Bullet_Display shape= bullet_pool.Get();
            shape.Init(disable);
        }   
    }

    private void disable(Bullet_Display shape)
    {
        bullet_pool.Release(shape);
    }
}
