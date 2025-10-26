using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MyMonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject.TryGetComponent<DragItem>(out var dragItem))
        {
            // Kiểm tra xem slot hiện tại đã có item chưa
            if (this.transform.childCount > 0)
            {
                Transform existingItem = this.transform.GetChild(0);
                if (existingItem.TryGetComponent<DragItem>(out var existingDragItem))
                {
                    // Hoán đổi parent của 2 items
                    Transform originalParent = dragItem.OriginalParent;
                    Debug.Log($"originalParent: {originalParent.name}");
                    existingDragItem.SetOriginalParent(originalParent);
                    existingDragItem.transform.SetParent(originalParent);
                    dragItem.SetOriginalParent(this.transform);
                    return;
                }
            }

            // Nếu slot trống, đặt item vào slot
            dragItem.SetOriginalParent(this.transform);
        }
    }
}
