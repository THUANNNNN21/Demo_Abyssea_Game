using System.Collections.Generic;
using UnityEngine;

public class HotKeyController : MyMonoBehaviour
{
    private static HotKeyController instance;
    public static HotKeyController Instance => instance;
    [SerializeField] private List<ItemSlot> itemSlots;
    public List<ItemSlot> ItemSlots => itemSlots;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemSlots();
    }
    private void LoadItemSlots()
    {
        if (itemSlots == null || itemSlots.Count == 0)
        {
            itemSlots = new List<ItemSlot>(GetComponentsInChildren<ItemSlot>());
        }
    }
}
