using System.Collections.Generic;
using System;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemLooter : MyMonoBehaviour
{
    public static event Action AfterPickupItem;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D lootingCollider;

    [SerializeField] private List<AbleToPickup> nearbyItems = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryController();
        this.LoadRigidbody2D();
        this.LoadLootingCollider();
    }

    private void LoadInventoryController()
    {
        if (this.inventoryController != null) return;
        this.inventoryController = GetComponentInParent<InventoryController>();
    }

    private void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.gravityScale = 0;
        this.rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void LoadLootingCollider()
    {
        if (this.lootingCollider != null) return;
        this.lootingCollider = GetComponent<CircleCollider2D>();
        this.lootingCollider.isTrigger = true;
        this.lootingCollider.radius = 0.8f;
    }

    private void OnEnable()
    {
        InputManager.OnPickupItem += PickupItem;
    }

    private void OnDisable()
    {
        InputManager.OnPickupItem -= PickupItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AbleToPickup>(out var ableToPickup))
        {
            if (!nearbyItems.Contains(ableToPickup))
            {
                nearbyItems.Add(ableToPickup);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AbleToPickup>(out var ableToPickup))
        {
            if (nearbyItems.Contains(ableToPickup))
            {
                nearbyItems.Remove(ableToPickup);
            }
        }
    }

    private void PickupItem()
    {
        PickupNearestItem();
        AfterPickupItem?.Invoke();
    }

    // ✅ Pickup item gần nhất
    private void PickupNearestItem()
    {
        if (nearbyItems.Count == 0)
        {
            return;
        }

        // Tìm item gần nhất
        AbleToPickup nearestItem = null;
        float nearestDistance = float.MaxValue;

        foreach (var item in nearbyItems)
        {
            if (item == null) continue;

            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestItem = item;
            }
        }
        if (nearestItem != null && IsAddItem(nearestItem))
        {
            nearestItem.PickedUp();
            nearbyItems.Remove(nearestItem);
            Debug.Log($"Picked up: {nearestItem.transform.parent.name}");
        }
    }

    // ✅ Alternative: Pickup tất cả items
    // private void PickupAllItems()
    // {
    //     if (nearbyItems.Count == 0) return;

    //     // Tạo copy để avoid modification during iteration
    //     var itemsToPickup = new List<AbleToPickup>(nearbyItems);

    //     foreach (var item in itemsToPickup)
    //     {
    //         if (item != null && IsAddItem(item))
    //         {
    //             item.PickedUp();
    //             nearbyItems.Remove(item);
    //             Debug.Log($"Picked up: {item.name}");
    //         }
    //     }
    // }

    private bool IsAddItem(AbleToPickup ableToPickup)
    {
        if (ableToPickup == null) return false;

        var itemInInventory = ableToPickup.GetItemInInventory();
        return this.inventoryController.Inventory.AddItem(
            itemInInventory,
            itemInInventory.upgradeLevel,
            1
        );
    }

    private void Update()
    {
        nearbyItems.RemoveAll(item => item == null);
    }
    public void SetLootRange(float range)
    {
        if (this.lootingCollider != null)
        {
            this.lootingCollider.radius = range;
        }
    }
}