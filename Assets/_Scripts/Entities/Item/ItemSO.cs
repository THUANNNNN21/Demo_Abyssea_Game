using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemID itemID = ItemID.None;
    public ItemType itemType = ItemType.None;
    public Sprite sprite;
    public SkillType skillType = SkillType.None;
    public int defaultMaxStack = 3;
    public List<ItemRecipe> levelUpRecipes;
}
