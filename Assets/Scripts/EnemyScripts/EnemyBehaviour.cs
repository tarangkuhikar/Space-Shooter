using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData EnemyData;
    [SerializeField]
    HealthSystem enemyhealth;
    [SerializeField]
    EnemyHealthbar enemyhealthbar;
    [SerializeField]
    float stoppos = 1;
    [SerializeField]
    GunScript[] guns;
    private void Start()
    {
        
        enemyhealth = new HealthSystem(EnemyData.Health);
        enemyhealth.OnHealthOver += Enemyhealth_OnHealthOver;
        enemyhealthbar.Setup(enemyhealth);
        foreach(GunScript gun in guns){
            gun.Changebullet(EnemyData.BulletIndex);
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, EnemyData.Speed);
    }

    private void Enemyhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        ScoreScript.ScoreChanged(EnemyData.Experience);
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= stoppos)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            enemyhealth.Damage(EnemyData.TakeDamage);
            collision.gameObject.SetActive(false);
        }
    }

}
