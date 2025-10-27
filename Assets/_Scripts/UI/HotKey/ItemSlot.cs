using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MyMonoBehaviour, IDropHandler
{
    [SerializeField] private bool isHotKeySlot = false;
    [SerializeField] private bool isDropable = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadIsHotKeySlot();
        this.LoadIsDropable();
    }
    private void LoadIsHotKeySlot()
    {
        if (this.gameObject.GetComponentInParent<HotKeyController>() != null)
        {
            isHotKeySlot = true;
        }
        else
        {
            isHotKeySlot = false;
        }
    }
    private void LoadIsDropable()
    {
        if (this.gameObject.name.Contains("DropPlace"))
        {
            isDropable = true;
        }
        else
        {
            isDropable = false;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject.TryGetComponent<DragItem>(out var dragItem))
        {
            InventoryController inventoryCtlr = PlayerController.Instance.InventoryController;
            inventoryCtlr.Inventory.SetItemSlotType(dragItem.ID, this.isHotKeySlot);

            Transform originalParent = dragItem.OriginalParent;
            // Nếu là slot drop, thực hiện drop item
            if (isDropable)
            {
                dragItem.transform.SetParent(originalParent);
                dragItem.ItemImage.raycastTarget = true;
                InventoryDrop inventoryDrop = PlayerController.Instance.InventoryController.InventoryDrop;
                if (inventoryDrop != null)
                {
                    inventoryDrop.DropItem(dragItem.ID);
                    Debug.Log(dragItem.transform.parent);
                    Debug.Log("Item dropped!");
                }
                return;
            }

            if (this.transform.childCount > 0)
            {
                Transform existingItem = this.transform.GetChild(0);
                if (existingItem.TryGetComponent<DragItem>(out var existingDragItem))
                {
                    if (dragItem.ItemImage.sprite.name == existingDragItem.ItemImage.sprite.name &&
                        dragItem.CurrentLevel == existingDragItem.CurrentLevel)
                    {
                        inventoryCtlr.ItemUpgrade.UpgradeItem(existingDragItem.ID);
                        Debug.Log("Items upgraded");
                        return;
                    }
                    existingDragItem.SetOriginalParent(originalParent);
                    existingDragItem.transform.SetParent(originalParent);
                    Debug.Log("existingDragItem new parent: " + existingDragItem.transform.parent);
                    inventoryCtlr.Inventory.SetItemSlotType(existingDragItem.ID, originalParent.GetComponent<ItemSlot>().isHotKeySlot);
                    dragItem.SetOriginalParent(this.transform);

                    return;
                }
            }
            dragItem.SetOriginalParent(this.transform);
            Debug.Log("dragItem new parent: " + dragItem.transform.parent);
        }
    }
}
