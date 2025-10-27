using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemName itemName = ItemName.None;
    public ItemType itemType = ItemType.None;

    [Header("Equipment Settings")]
    public ItemModelType itemModelType = ItemModelType.None;

    [Header("Item Info")]
    public Sprite sprite;
    public SkillType skillType = SkillType.None;
    public int defaultMaxStack = 3;
    public List<ItemRecipe> levelUpRecipes;

    // Validation
    private void OnValidate()
    {
        // Nếu không phải Equipment, reset ItemModelType về None
        if (itemType != ItemType.Equipment)
        {
            itemModelType = ItemModelType.None;
        }
    }
}
