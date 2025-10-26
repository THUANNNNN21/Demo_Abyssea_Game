using UnityEngine;

public class UIInventoryController : MyMonoBehaviour
{

    [SerializeField] private UIItemSpawner uiInventorySpawner;
    public UIItemSpawner UIInventorySpawner => uiInventorySpawner; [SerializeField] private UISlotSpawner uiSlotInventorySpawner;
    public UISlotSpawner UISlotInventorySpawner => uiSlotInventorySpawner;

    [SerializeField] private GameObject contentHolder;
    public GameObject ContentHolder => contentHolder;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUISlotInventorySpawner();
        this.LoadUIInventorySpawner();
        this.LoadContentHolder();
    }
    private void LoadUISlotInventorySpawner()
    {
        if (this.uiSlotInventorySpawner != null) return;
        this.uiSlotInventorySpawner = this.GetComponentInChildren<UISlotSpawner>();
        Debug.LogWarning(this.gameObject.name + ": Load UISlotInventorySpawner");
    }
    private void LoadUIInventorySpawner()
    {
        if (this.uiInventorySpawner == null)
        {
            this.uiInventorySpawner = this.GetComponentInChildren<UIItemSpawner>();
        }
    }
    private void LoadContentHolder()
    {
        if (this.contentHolder == null)
        {
            Transform contentHolderTransform = this.transform.Find("Scroll View/Viewport/Content");
            if (contentHolderTransform != null)
            {
                this.contentHolder = contentHolderTransform.gameObject;
            }
        }
    }
}
