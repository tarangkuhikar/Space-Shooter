using DG.Tweening;
using UnityEngine;

public class Level5Boss : BossScript
{
    int health = 300;

    public override void StartBossFight()
    {
        transform.DOMoveY(3,4).SetEase(Ease.Linear).OnComplete(()=>StartFiring());    
    }

    private void StartFiring()
    {
             
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 10;
            collision.gameObject.SetActive(false);
            
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
