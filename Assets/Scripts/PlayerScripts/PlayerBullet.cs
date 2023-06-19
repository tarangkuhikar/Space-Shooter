using UnityEngine;
using UnityEngine.UI;
public class PlayerBullet : MonoBehaviour
{
    Image image;
    // Start is called before the first frame update
    void Awake()
    {
       image= GetComponent<Image>();
    }

   public void BulletSprite(Sprite s)
    {
        image.sprite = s;
    }
}
