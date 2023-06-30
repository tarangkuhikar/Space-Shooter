using UnityEngine;
using UnityEngine.UI;
public class PlayerBulletUI : MonoBehaviour
{
    Image _image;
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void BulletSprite(Sprite s)
    {
        _image.sprite = s;
    }
}
