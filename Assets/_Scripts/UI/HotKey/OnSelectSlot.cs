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
        Debug.LogWarning($"Load HotKeyController in {this.name} ", this);
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
            Debug.Log($"🔥 Calling OnSelect() of selected item: {currentSelectedItem.name}");
            currentSelectedItem.OnSelect();
        }
        else
        {
            Debug.Log("No item selected to cast");
        }
    }

    private void Select(int slotNumber)
    {
        Debug.Log($"Slot {slotNumber + 1} selected", this);
        ItemSlot slot = HotKeyController.ItemSlots[slotNumber];
        AbleToSelect ableToSelect = slot.GetComponentInChildren<AbleToSelect>();

        if (ableToSelect == null)
        {
            Debug.Log($"No AbleToSelect found in slot {slotNumber + 1}", this);
            currentSelectedItem = null;
        }
        else
        {
            Debug.Log($"✅ Item ready: {ableToSelect.name}");
            currentSelectedItem = ableToSelect;
        }
    }
}