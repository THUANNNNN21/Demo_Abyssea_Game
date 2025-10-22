using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string playerName = "Player";
    public int maxHP;
    public int speed;
    public int InventorySize;
}
