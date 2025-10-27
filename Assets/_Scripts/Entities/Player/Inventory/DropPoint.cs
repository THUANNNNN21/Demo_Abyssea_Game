using UnityEngine;

#region Header
public class DropPoint : MyMonoBehaviour
{
    #region Properties
    [SerializeField] private InventoryController inventoryController;
    public InventoryController InventoryController { get => inventoryController; }
    #endregion

    #region Unity Methods
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryController();
    }

    protected override void LoadValues()
    {
        base.LoadValues();
        this.transform.position = new Vector3(-1f, 0f, 0f);
    }
    #endregion

    #region Load Methods
    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        else
        {
            this.inventoryController = GetComponentInParent<InventoryController>();
        }
    }
    #endregion
}
#endregion
