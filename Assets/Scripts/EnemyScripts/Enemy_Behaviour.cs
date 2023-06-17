using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    HealthSystem enemyhealth = new HealthSystem(20);
    [SerializeField]
    private EnemyHealthbar HealthBar;

    [SerializeField]
    GunScript[] enemyGun;

    [SerializeField]
    float enemyspeed;
    int bullet_index;

    float t = 0;

    [SerializeField]
    float timetoshoot = 5;

    public static float score;

    private void Start()
    {
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
        HealthBar.Setup(enemyhealth);

        bullet_index = Random.Range(2, 64);

        enemyGun[0].Changebullet(bullet_index);
        enemyGun[1].Changebullet(bullet_index);
    }

    private void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        score++;
    }

    void Update()
    {
        t += Time.deltaTime;
        transform.position += new Vector3(0, enemyspeed * Time.deltaTime, 0);
        if (t > timetoshoot)
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
