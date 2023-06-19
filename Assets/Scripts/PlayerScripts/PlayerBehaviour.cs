using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{   [SerializeField]
    PlayerData playerdata;
    [SerializeField]
    private HealthBar healthBar;
    HealthSystem playerhealth = new HealthSystem(100);
    [SerializeField]
    GunScript[] Playergun;

    public static event Action PlayerDied;

    private int playerDeath = 0;
    [SerializeField]
    PlayerBullet p;
    private void Start()
    {
        healthBar.Setup(playerhealth);
        playerhealth.OnHealthOver += Playerhealth_OnHealthOver;
        Playergun[0].Changebullet(playerdata.Bullet);
        Playergun[1].Changebullet(playerdata.Bullet);
        p.BulletSprite(Bullet_Sprite.GetSprite(1 + 4 * (playerdata.Bullet - 1) + 5 * (int)Mathf.Ceil(playerdata.Bullet / 7)));
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
            playerdata.Bullet = UnityEngine.Random.Range(2, 64);
            Playergun[0].Changebullet(playerdata.Bullet);
            Playergun[1].Changebullet(playerdata.Bullet);
            p.BulletSprite(Bullet_Sprite.GetSprite(1 + 4 * (playerdata.Bullet - 1) + 5 * (int)Mathf.Ceil(playerdata.Bullet / 7)));

        }
    }

    private void Playerhealth_OnHealthOver(object sender, System.EventArgs e)
    {
        playerDeath += 1;
        Debug.Log(playerDeath);
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
