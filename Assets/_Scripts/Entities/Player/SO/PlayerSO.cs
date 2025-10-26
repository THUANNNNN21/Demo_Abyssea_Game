using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName = "Player";
    public int maxHP;
    public int damage;
    public float attackRange;
    public float attackDelay;
    public int speed;
    public int inventorySize;
    public float lootRange;
}
