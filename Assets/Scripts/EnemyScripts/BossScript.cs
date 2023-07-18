using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    protected EnemyData _enemyData;

    [SerializeField]
    protected GunScript[] _guns;
    protected int _health;
    protected void OnEnable()
    {
        _health = _enemyData.Experience;
    }

    public virtual void StartBossFight()
    { }

    protected void OnDestroy()
    {
        ScoreScript.ScoreChanged(_enemyData.Experience);
        transform.DOKill();
        StopAllCoroutines();
        StartCoroutine(LoadAfter1Sec());
    }

    IEnumerator LoadAfter1Sec()
    {
        yield return new WaitForSeconds(1);
    }
}
