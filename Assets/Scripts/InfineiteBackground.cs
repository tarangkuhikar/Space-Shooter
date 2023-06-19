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

    private void Update()
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(0, movespeed) * Time.deltaTime, background.uvRect.size);
    }
}
