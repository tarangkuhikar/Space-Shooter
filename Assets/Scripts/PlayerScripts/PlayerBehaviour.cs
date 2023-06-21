using UnityEngine;
using System;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{   [SerializeField]
    PlayerData playerdata;

    [SerializeField]
    private HealthBar healthBar;
    HealthSystem playerhealth;

    [SerializeField]
    GunScript[] Playergun;

    public static event Action PlayerDied;

    [SerializeField]
    PlayerBulletUI PlayerBulletUI;

    [SerializeField]
    int playerlives=3;

    [SerializeField]
    TMP_Text lives;
    private void Start()
    {
        playerhealth = new HealthSystem(playerdata.Health);
        healthBar.Setup(playerhealth);
        lives.text = playerlives.ToString();
        playerhealth.OnHealthOver += Playerhealth_OnHealthOver;

        
        foreach(GunScript gun in Playergun)
        {
            gun.Changebullet(playerdata.Bullet);
        }
        PlayerBulletUI.BulletSprite(BulletSprite.GetSprite(playerdata.Bullet*4));
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            foreach(GunScript guns in Playergun)
            {
                guns.Fire();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerdata.Bullet = UnityEngine.Random.Range(0,77);
            Playergun[0].Changebullet(playerdata.Bullet);
            Playergun[1].Changebullet(playerdata.Bullet);

            PlayerBulletUI.BulletSprite(BulletSprite.GetSprite(playerdata.Bullet*4));

        }
    }

    private void Playerhealth_OnHealthOver(object sender,EventArgs e)
    {
        playerlives-=1;
        playerhealth.Heal(playerdata.Health);
        lives.text = playerlives.ToString();
        if (playerlives == 0)
        {
            Destroy(gameObject);
        }       
        PlayerDied?.Invoke();
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
