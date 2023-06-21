using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData")]
public class PlayerData :ScriptableObject
{
    public int Health;
    public float Speed;
    public int Bullet;
    public float BulletSpeed;
}
