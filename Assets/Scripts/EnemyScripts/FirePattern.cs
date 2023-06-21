using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(GunScript))]
public class FirePattern : MonoBehaviour
{
    [SerializeField]
    private GunScript _gun;
    [SerializeField]
    float _waittime;
    [SerializeField]
    float _rotateTime;
    [SerializeField]
    Vector3 _rotationValue;

    private void OnEnable()
    {
        StartCoroutine(StartFire());
        Sequence s=DOTween.Sequence();
        s.Append(transform.DORotate(_rotationValue, _rotateTime, RotateMode.LocalAxisAdd).SetLoops(2,LoopType.Yoyo).SetEase(Ease.Linear));
        s.Append(transform.DORotate(-_rotationValue, _rotateTime, RotateMode.LocalAxisAdd).SetLoops(2,LoopType.Yoyo).SetEase(Ease.Linear));
        s.SetLoops(-1);
    }
    IEnumerator StartFire()
    {
        while (true)
        {
            _gun.Fire();
            yield return new WaitForSeconds(_waittime);
        }
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
