using UnityEngine;
using UnityEngine.UI;

public class InfineiteBackground : MonoBehaviour
{
    RawImage _backGround;

    Vector2 _moveSpeed = new Vector2(0, 0.01f);
    private void Start()
    {
        _backGround = GetComponent<RawImage>();
    }

    private void FixedUpdate()
    {
        _backGround.uvRect = new Rect(_backGround.uvRect.position + _moveSpeed, _backGround.uvRect.size);
    }
}
