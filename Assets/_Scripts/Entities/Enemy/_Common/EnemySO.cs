using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public int damage;
    public float speed;
    public float maxDistance;
    public List<ItemDropRate> dropList;
    public bool isDestroyWhenImpact = false;
}
