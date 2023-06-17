using System.Collections;
using UnityEngine;

public class Bullet_Sprite : MonoBehaviour
{
    static Sprite[] bullet_sprite;
    Sprite s;

    private void Start()
    {
        Debug.Log("start called");
        bullet_sprite = Resources.LoadAll<Sprite>("Bullets_asset_folder/Bullet_00");
        s=bullet_sprite[200];
    }

    public static Sprite GetSprite(int index)
    {
        return bullet_sprite[index];
    }
}
