using UnityEngine;


public class BulletSprite : MonoBehaviour
{
    static Sprite[] _bulletSprite;

    public void Awake()
    {
        _bulletSprite = Resources.LoadAll<Sprite>("BulletAssets");
    }

    public static Sprite GetSprite(int index)
    {
        return _bulletSprite[index];
    }
}
