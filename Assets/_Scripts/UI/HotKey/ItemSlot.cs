using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MyMonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject droppedObject = eventData.pointerDrag;
        DragItem dragItem = droppedObject.GetComponent<DragItem>();
        if (dragItem != null)
        {
            dragItem.SetOriginnalParent(this.transform);
        }
    }
}
