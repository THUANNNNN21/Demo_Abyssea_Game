using UnityEngine;

[RequireComponent(typeof(DespawnObject))]
public class ItemController : MyMonoBehaviour
{
    [SerializeField] private ItemInInventory itemInInventory;
    public ItemInInventory ItemInInventory => itemInInventory;
    [SerializeField] private DespawnObject despawnObject;
    public DespawnObject DespawnObject => despawnObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemSO();
        this.LoadDespawnObject();
        this.LoadSpriteRenderer();
    }
    private void LoadItemSO()
    {
        if (itemInInventory.itemSO != null) return;
        else
        {
            itemInInventory.itemSO = Resources.Load<ItemSO>("_SO/ItemSO/" + this.name);
        }
    }
    private void LoadDespawnObject()
    {
        if (this.despawnObject != null) return;
        this.despawnObject = GetComponent<DespawnObject>();
    }
    private void LoadSpriteRenderer()
    {
        if (this.spriteRenderer != null) return;
        this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (this.spriteRenderer != null && this.itemInInventory.itemSO != null)
        {
            this.spriteRenderer.sprite = this.itemInInventory.itemSO.sprite;
        }
    }
    public void SetInventory(ItemInInventory itemInInventory)
    {
        this.itemInInventory = itemInInventory;
    }
}
