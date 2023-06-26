using UnityEngine;
using UnityEngine.UI;

public class InfineiteBackground : MonoBehaviour
{
    RawImage background;
    [SerializeField]
    float movespeed;
    private void Start()
    {
        background = GetComponent<RawImage>();
    }

    private void FixedUpdate()
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(0, movespeed) , background.uvRect.size);
    }
}
