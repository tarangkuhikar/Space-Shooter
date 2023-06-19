using UnityEngine;
using System;

public class Enemy_Behaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData enemydata;

    HealthSystem enemyhealth = new HealthSystem(20);
    [SerializeField]
    private EnemyHealthbar HealthBar;

    [SerializeField]
    GunScript[] enemyGun;

    float t = 0;
    int index;
    public static event Action<int> ScoreChanged;
    private void Start()
    {
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
        HealthBar.Setup(enemyhealth);

        index= UnityEngine.Random.Range(2, 64);

        enemyGun[0].Changebullet(index);
        enemyGun[1].Changebullet(index);
    }

    private void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        
        ScoreChanged(enemydata.Experience);
    }

    void Update()
    {
        t += Time.deltaTime;
        transform.position += new Vector3(0, enemydata.speed* Time.deltaTime, 0);
        if (t > enemydata.FireSpeed)
        {
            enemyGun[0].Fire();
            enemyGun[1].Fire();
            t = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerBullet"))
        {
            enemyhealth.Damage(10);
            other.gameObject.SetActive(false);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }
}
