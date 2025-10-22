using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : MyMonoBehaviour
{
    public static event Action AfterUpgradeItem;
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryController();
    }
    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        inventoryController = GetComponentInParent<InventoryController>();
    }
    private void OnEnable()
    {
        InputManager.OnUpgradeItem += HandleUpgradeItem;
    }
    private void OnDisable()
    {
        InputManager.OnUpgradeItem -= HandleUpgradeItem;
    }
    private void HandleUpgradeItem(int itemPosition)
    {
        if (itemPosition < 1 || itemPosition > 9) return;
        int itemIndex = itemPosition - 1;
        this.UpgradeItem(itemIndex);
        AfterUpgradeItem?.Invoke();
    }
    public bool UpgradeItem(int itemIndex)
    {
        if (itemIndex >= this.InventoryController.ListItems.Count || itemIndex < 0) return false;
        ItemInInventory itemInInventory = this.InventoryController.ListItems[itemIndex];
        if (itemInInventory.itemSO.itemType != ItemType.Equipment)
        {
            Debug.Log("Item is not equipment");
            return false;
        }
        if (itemInInventory.itemsCount < 1) return false;
        List<ItemRecipe> upgradeLevels = itemInInventory.itemSO.levelUpRecipes;
        if (!this.ItemUpgradeable(upgradeLevels, itemIndex)) return false;
        if (!this.HaveEnoughIngredients(itemInInventory.upgradeLevel, upgradeLevels)) return false;
        this.DeductIngredients(itemInInventory.upgradeLevel, upgradeLevels);
        this.InventoryController.AddItem(itemInInventory, itemInInventory.upgradeLevel + 1, 1);
        return true;
    }
    private bool ItemUpgradeable(List<ItemRecipe> upgradeLevels, int itemIndex)
    {
        if (upgradeLevels == null) return false;
        if (upgradeLevels.Count < 1) return false;
        if (this.InventoryController.ListItems[itemIndex].upgradeLevel >= upgradeLevels.Count)
        {
            Debug.Log("Max level reached");
            return false;
        }
        return true;
    }
    private bool HaveEnoughIngredients(int currentUpgradeLevel, List<ItemRecipe> upgradeLevels)
    {
        int itemCount;
        int itemLevel;
        ItemID ingredientID;
        if (currentUpgradeLevel > upgradeLevels.Count) return false;
        ItemRecipe currentLevel = upgradeLevels[currentUpgradeLevel];
        foreach (ItemIngredient ingredient in currentLevel.ingredients)
        {
            ingredientID = ingredient.itemSO.itemID;
            itemCount = ingredient.amount;
            itemLevel = ingredient.itemLevel;
            if (!InventoryController.ItemCheck(ingredientID, itemLevel, itemCount)) return false;
        }
        return true;
    }
    private void DeductIngredients(int currentUpgradeLevel, List<ItemRecipe> upgradeLevels)
    {
        int itemCount;
        int itemLevel;
        ItemID ingredientID;
        ItemRecipe currentLevel = upgradeLevels[currentUpgradeLevel];
        foreach (ItemIngredient ingredient in currentLevel.ingredients)
        {
            ingredientID = ingredient.itemSO.itemID;
            itemCount = ingredient.amount;
            itemLevel = ingredient.itemLevel;
            this.InventoryController.DeductItem(ingredientID, itemLevel, itemCount);
        }
    }
}
