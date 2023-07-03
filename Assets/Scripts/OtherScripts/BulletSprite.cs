using UnityEngine;


public class BulletSprite : MonoBehaviour
{
    static Sprite[] _bulletSprite;

    void Awake()
    {
        _bulletSprite = Resources.LoadAll<Sprite>("BulletAssets");
    }

    /// <summary>
    /// Get a sprite from the sprite sheet in resources folder.
    /// </summary>
    /// <param name="index">The index of the sprite</param>
    /// <returns>The sprite at the index.</returns>
    public static Sprite GetSprite(int index)
    {
        return _bulletSprite[index];
    }
}
