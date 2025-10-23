using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MyMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform originalParent;
    [SerializeField] private Image image;
    public Image Image => image;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }
    private void LoadImage()
    {
        if (this.image != null) return;
        this.image = GetComponent<Image>();
        Debug.LogWarning($"Load Image in {this.name} ", this);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.originalParent = this.transform.parent;
        this.transform.SetParent(HotKeyController.Instance.transform);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        this.transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(this.originalParent);
        image.raycastTarget = true;
    }
    public void SetOriginnalParent(Transform parent)
    {
        this.originalParent = parent;
    }
}
