using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    HealthSystem enemyhealth = new HealthSystem(20);

    private void Start()
    {
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
    }

    private void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += new Vector3(0,-0.01f,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            enemyhealth.Damage(10);
            other.gameObject.SetActive(false);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
