using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    HealthSystem playerhealth = new HealthSystem(100);
    [SerializeField]
    GunScript[] Playergun;
    int bulletindex;

    public static event Action PlayerDied; 

    private void Start()
    {
        healthBar.Setup(playerhealth);
        playerhealth.OnHealthOver += Playerhealth_OnHealthOver;
        Playergun[0].Changebullet(3);
        Playergun[1].Changebullet(3);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Playergun[0].Fire();
            Playergun[1].Fire();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            bulletindex = UnityEngine.Random.Range(2, 64);
            Playergun[0].Changebullet(bulletindex);
            Playergun[1].Changebullet(bulletindex);

        }
    }

    private void Playerhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        Debug.Log("game over");
        PlayerDied?.Invoke();
        playerhealth.Heal(100);
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
