using DG.Tweening;
using UnityEngine;

public class Level5Boss : BossScript
{

    public override void StartBossFight()
    {
        transform.DOMoveY(3, 4).SetEase(Ease.Linear).OnComplete(() => StartFiring());
    }

    private void StartFiring()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _health -= 10;
            collision.gameObject.SetActive(false);
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
