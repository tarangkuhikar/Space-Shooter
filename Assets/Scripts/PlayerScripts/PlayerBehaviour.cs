using UnityEngine;
using System;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    PlayerData playerdata;

    [SerializeField]
    GunScript[] Playergun;

    public static event Action PlayerDied;


    [SerializeField]
    PlayerBulletUI PlayerBulletUI;

    [SerializeField]
    int playerlives = 3;

    float t = 0;

   
    private void Start()
    {
        PlayerBulletUI.BulletSprite(BulletSprite.GetSprite(playerdata.Bullet * 4));
    }

    public void FixedUpdate()
    {
        t += Time.deltaTime;
        if (Input.GetButton("Fire1") && t >= playerdata.FireRate)
        {
            t = 0;
            foreach (GunScript guns in Playergun)
            {
                guns.Fire(playerdata.BulletSpeed,playerdata.Bullet);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            playerlives-= 1;
            other.gameObject.SetActive(false);
            LivesScript.LivesChanged(playerlives);
        }

        if (playerlives == 0)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
            PlayerDied();
        }
    }
}
