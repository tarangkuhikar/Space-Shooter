using System.Collections;
using UnityEngine;

public class Bullet_Sprite : MonoBehaviour
{
    static Sprite[] bullet_sprite=new Sprite[300];

    private void Start()
    {
        bullet_sprite = Resources.LoadAll<Sprite>("Bullets_asset_folder/Bullet_00");
    }

    public static Sprite GetSprite(int index)
    {
        return bullet_sprite[4*(index-1)+5*(int)(Mathf.Ceil(index/7))];
    }
}
