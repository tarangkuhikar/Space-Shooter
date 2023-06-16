using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{[SerializeField]
    private HealthBar healthBar;
    HealthSystem playerhealth = new HealthSystem(100);
    [SerializeField]
    GunScript[] Playergun;

    private void Start()
    {
        healthBar.Setup(playerhealth);
        playerhealth.OnHealthOver += Playerhealth_OnHealthOver;
        Playergun[0].Changebullet(3);
        Playergun[1].Changebullet(3);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1")) { 
        Playergun[0].Fire();
        Playergun[1].Fire();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Playergun[0].Changebullet(Random.Range(2,64));
            Playergun[1].Changebullet(Random.Range(2, 64));

        }
    }

    private void Playerhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        Debug.Log("game over");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("EnemyBullet"))
        {
            playerhealth.Damage(10);
            other.gameObject.SetActive(false);
        }
    }
}
