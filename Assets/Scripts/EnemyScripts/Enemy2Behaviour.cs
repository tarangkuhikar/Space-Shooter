using System;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy2Behaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData enemydata;

    HealthSystem enemyhealth = new HealthSystem(200);
    [SerializeField]
    private EnemyHealthbar HealthBar;

    [SerializeField]
    GunScript[] enemyGun;

    float t = 0;
    [SerializeField]
    int index;
    public static event Action<int> ScoreChanged;
    [SerializeField]
    int firerate;
    bool vulnerable = false;
    [SerializeField]
    float stoppos;
    [SerializeField]
    bool IsFiring = false;

    private void Start()
    {
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
        HealthBar.Setup(enemyhealth);

        foreach (GunScript gun in enemyGun)
        {
            gun.Changebullet(index);
        }
    }

    private void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        ScoreChanged(enemydata.Experience);
        Destroy(gameObject);
    }

    void Update()
    {
        t += Time.deltaTime;
        if (transform.position.y >= stoppos)
        {
            transform.position += new Vector3(0, enemydata.speed * Time.deltaTime, 0);
        }
        if (IsFiring == false)
        {
            if (t > enemydata.FireSpeed)
            {
                int x = UnityEngine.Random.Range(0, 4);
                enemyGun[x].InvokeRepeating("Fire", 2, 2);
            }
        }
        else
        {
            t = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerBullet") && vulnerable == true)
        {
            enemyhealth.Damage(10);
            other.gameObject.SetActive(false);
        }
        other.gameObject.SetActive(false);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }
}
