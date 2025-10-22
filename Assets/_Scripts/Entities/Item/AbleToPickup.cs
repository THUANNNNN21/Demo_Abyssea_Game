using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class AbleToPickup : MyMonoBehaviour
{
    [SerializeField] private CircleCollider2D pickupCollider;
    [SerializeField] private ItemController itemController;
    public ItemController ItemController { get => itemController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPickupCollider();
        this.LoadItemController();
    }
    private void LoadItemController()
    {
        if (this.itemController != null) return;
        this.itemController = GetComponentInParent<ItemController>();
    }
    private void LoadPickupCollider()
    {
        if (this.pickupCollider != null) return;
        this.pickupCollider = GetComponent<CircleCollider2D>();
        this.pickupCollider.isTrigger = true;
    }
    public ItemInInventory GetItemInInventory()
    {
        return this.itemController.ItemInInventory;
    }
    public void PickedUp()
    {
        this.itemController.DespawnObject.HandleDespawn();
    }

}
