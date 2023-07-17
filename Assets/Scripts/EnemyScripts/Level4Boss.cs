using UnityEngine;
using DG.Tweening;

public class Level4Boss : BossScript
{
    int health = 175;
    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartFiring());
    }

    private void StartFiring()
    {
        //Enemy pattern
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 10;
            collision.gameObject.SetActive(false);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
