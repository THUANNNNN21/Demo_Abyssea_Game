using UnityEngine;

public class DropPoint : MyMonoBehaviour
{
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
        else
        {
            this.inventoryController = GetComponentInParent<InventoryController>();
        }
    }
    protected override void LoadValues()
    {
        base.LoadValues();
        this.transform.position = new Vector3(-1f, 0f, 0f);
    }
}
