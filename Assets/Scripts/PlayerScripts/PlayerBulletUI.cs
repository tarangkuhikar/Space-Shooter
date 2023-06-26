using UnityEngine;
using UnityEngine.UI;
public class PlayerBulletUI : MonoBehaviour
{
    Image image;
    void Awake()
    {
       image= GetComponent<Image>();
    }

   public void BulletSprite(Sprite s)
    {
        image.sprite = s;
    }
}
