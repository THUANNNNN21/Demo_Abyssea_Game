using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MyMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform originalParent;
    public Transform OriginalParent => originalParent;
    [SerializeField] private Image itemImage;
    public Image ItemImage => itemImage;
    [SerializeField] private string id;
    public string ID => id;
    [SerializeField] private int currentLevel;
    public int CurrentLevel => currentLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemImage();
    }
    private void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = transform.Find("ItemImage").GetComponent<Image>();
        Debug.LogWarning($"[{this.gameObject.name}] ItemImage not found, please check again!");
    }
    void Start()
    {
        this.originalParent = this.transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.originalParent = this.transform.parent;
        this.transform.SetParent(HotKeyController.Instance.transform);
        itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = InputManager.Instance.GetMouseWorldPosition();
        this.transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(this.originalParent);
        itemImage.raycastTarget = true;
    }
    public void SetOriginalParent(Transform parent)
    {
        this.originalParent = parent;
    }
    public void SetID(string id)
    {
        this.id = id;
    }
    public void SetCurrentLevel(int level)
    {
        this.currentLevel = level;
    }
}
