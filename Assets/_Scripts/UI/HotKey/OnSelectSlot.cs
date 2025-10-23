using System.Collections.Generic;
using UnityEngine;

public class OnSelectSlot : MyMonoBehaviour
{
    [SerializeField] private HotKeyController hotKeyController;
    public HotKeyController HotKeyController => hotKeyController;
    private AbleToSelect currentSelectedItem = null;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHotKeyController();
    }

    private void LoadHotKeyController()
    {
        if (hotKeyController != null) return;
        hotKeyController = this.GetComponentInParent<HotKeyController>();
    }

    private void OnEnable()
    {
        InputManager.OnItemSelected += HandleItemSelected;
        InputManager.OnItemCast += HandleItemCast; // ✅ Add this line
    }

    private void OnDisable()
    {
        InputManager.OnItemSelected -= HandleItemSelected;
        InputManager.OnItemCast -= HandleItemCast; // ✅ Add this line
    }

    private void HandleItemSelected(int index)
    {
        if (index == 0) this.Select(0);
        else if (index == 1) this.Select(1);
        else if (index == 2) this.Select(2);
        else if (index == 3) this.Select(3);
        else Debug.LogWarning($"Invalid item index selected: {index + 1}", this);
    }

    // ✅ Handle mouse click - only call selected item
    private void HandleItemCast()
    {
        if (currentSelectedItem != null)
        {
            currentSelectedItem.OnSelect();
        }
        else
        {
            Debug.Log("No item is currently selected to cast.", this);
        }
    }

    private void Select(int slotNumber)
    {
        ItemSlot slot = HotKeyController.ItemSlots[slotNumber];
        AbleToSelect ableToSelect = slot.GetComponentInChildren<AbleToSelect>();

        if (ableToSelect == null)
        {
            currentSelectedItem = null;
        }
        else
        {
            currentSelectedItem = ableToSelect;
        }
    }
}