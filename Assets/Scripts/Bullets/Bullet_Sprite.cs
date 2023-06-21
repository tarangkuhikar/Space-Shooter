using UnityEngine;

public class Bullet_Sprite : MonoBehaviour
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
