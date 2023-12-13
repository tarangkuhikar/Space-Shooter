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
        Invoke(nameof(LoadAfter1Sec), 1);       
    }

    void LoadAfter1Sec()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
