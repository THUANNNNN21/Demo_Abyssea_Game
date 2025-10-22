using UnityEngine;

public class UIInventoryController : MyMonoBehaviour
{
    [SerializeField] private UIInventorySpawner uiInventorySpawner;
    public UIInventorySpawner UIInventorySpawner => uiInventorySpawner;
    [SerializeField] private GameObject contentHolder;
    public GameObject ContentHolder => contentHolder;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIInventorySpawner();
        this.LoadContentHolder();
    }
    private void LoadUIInventorySpawner()
    {
        if (this.uiInventorySpawner == null)
        {
            this.uiInventorySpawner = this.GetComponentInChildren<UIInventorySpawner>();
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
