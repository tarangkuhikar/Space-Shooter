using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData")]
public class EnemyData : ScriptableObject
{
    public float Health;
    public float Speed;
    public int BulletIndex;
    public int Experience;
    public float TakeDamage;
}
