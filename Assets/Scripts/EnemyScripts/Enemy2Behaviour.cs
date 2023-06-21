using System;
using UnityEngine;
using System.Collections;
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
    [SerializeField]
    float waitime;
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
            vulnerable = false;
            if (t >= waitime)
            {
                int x = UnityEngine.Random.Range(0, 4);
                StartCoroutine(Startfiring(x));
            }
        }
        else
        {
            t = 0;
        }
    }

    IEnumerator Startfiring(int x)
    {
        IsFiring = true;
        vulnerable = true;
        for (int i = 0; i < firerate; i++)
        {
            Debug.Log("firing");
            enemyGun[x].Fire();
            Debug.Log("fired");
            yield return new WaitForSeconds(enemydata.FireSpeed);
        }
        IsFiring = false;
        vulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerBullet"))
        {
            if (vulnerable == true)
            {
                enemyhealth.Damage(10);
                other.gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }

        waitime *= enemyhealth.GetHealthPercentage();
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }
    public void OnDestroy()
    {
        enemyhealth.OnHealthOver -= Enemyhealth_OnHealthOver;
    }
}
