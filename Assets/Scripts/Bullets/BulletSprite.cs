using UnityEngine;

public class BulletSprite : MonoBehaviour
{
    static Sprite[] bullet_sprite;
    public void Awake()
    {
        bullet_sprite = Resources.LoadAll<Sprite>("BulletAssets");
    }

    public static Sprite GetSprite(int index)
    {
        return bullet_sprite[index];
    }
}
