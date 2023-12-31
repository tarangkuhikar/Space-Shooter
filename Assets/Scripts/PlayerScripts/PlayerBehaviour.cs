﻿using System;
using System.Collections;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    PlayerData playerdata;

    [SerializeField]
    GunScript[] _playerGun;

    public static event Action PlayerDied;

    [SerializeField]
    int _playerLives = 3;

    float _fireRate = 0;


    private void Start()
    {
        LivesScript.LivesChanged(_playerLives);
    }

    public void FixedUpdate()
    {
        _fireRate += Time.deltaTime;
        if (Input.GetButton("Fire1") && _fireRate >= playerdata.FireRate)
        {
            _fireRate = 0;
            foreach (GunScript guns in _playerGun)
            {
                if (guns.isActiveAndEnabled)
                {
                    guns.Fire(playerdata.BulletSpeed, playerdata.Bullet);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            _playerLives -= 1;
            other.gameObject.SetActive(false);
            LivesScript.LivesChanged(_playerLives);
        }

        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            StartCoroutine(AsteroidHit());
        }

        if (_playerLives == 0)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
            PlayerDied();
        }
    }
    
    IEnumerator AsteroidHit()
    {
        _playerGun[0].gameObject.SetActive(false);
       for(int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.35f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.35f);
        }
        _playerGun[0].gameObject.SetActive(true);
    }
}
