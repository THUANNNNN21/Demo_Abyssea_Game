using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    [Header("Enemy Damage Stats")]
    public int maxHP;
    public int damage;
    [Header("Enemy Movement Stats")]
    public float speed;
    public float maxDistance;
    public float targetRadius;
    public float fakeTargetRadius;
    public float distanceToPlayerForMove;
    [Header("Enemy Rewards")]
    public int expReward;
    public int scoreReward;
    public float timeReward;
    public List<ItemDropRate> dropList;

    private void OnValidate()
    {
        // Update names for all drop list items
        if (dropList != null)
        {
            foreach (var item in dropList)
            {
                if (item != null)
                {
                    item.OnValidate();
                }
            }
        }
    }
}
