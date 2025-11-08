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
    public int expReward;
    public int scoreReward;
    public float timeReward;
    public float targetRadius;
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
