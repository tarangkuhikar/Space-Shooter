using UnityEngine;

public class Bullet_Sprite : MonoBehaviour
{
    static Sprite[] bullet_sprite;
    public void Awake()
    {
        bullet_sprite = Resources.LoadAll<Sprite>("Bullets_asset_folder/Bullet_00");
    }

    public static Sprite GetSprite(int index)
    {
        return bullet_sprite[index];
    }
}
