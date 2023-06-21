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
    [SerializeField]
    float stoppos = 2;
    [SerializeField]
    float BulletSpeed;
    public static event Action<int> ScoreChanged;
    private void Start()
    {
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
        HealthBar.Setup(enemyhealth);
        index = UnityEngine.Random.Range(0, 77);

        foreach (GunScript gun in enemyGun)
        {
            gun.SetSpeed(BulletSpeed);
            gun.Changebullet(index);
        }
    }

    public void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        ScoreChanged?.Invoke(enemydata.Experience);
    }

    public void Update()
    {
        t += Time.deltaTime;
        if (transform.position.y >= stoppos)
        {
            transform.position += new Vector3(0, enemydata.speed * Time.deltaTime, 0);
        }
        if (t > enemydata.FireSpeed)
        {
            enemyGun[0].Fire();
            enemyGun[1].Fire();
            t = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerBullet"))
        {
            enemyhealth.Damage(10);
            other.gameObject.SetActive(false);
        }
    }
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
