using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIItem : MyMonoBehaviour
{
    [SerializeField] private Image itemImage;
    public Image ItemImage => itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    public TextMeshProUGUI ItemNameText => itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    public TextMeshProUGUI ItemCountText => itemCountText;
    [SerializeField] AbleToSelect ableToSelect;
    public AbleToSelect AbleToSelect => ableToSelect;
    [SerializeField] private DragItem dragItem;
    public DragItem DragItem => dragItem;
    protected override void LoadComponents()
    {
        this.LoadItemImage();
        this.LoadItemNameText();
        this.LoadItemCountText();
        this.LoadAbleToSelect();
        this.LoadDragItem();
    }
    private void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = transform.Find("ItemImage").GetComponent<Image>();
        Debug.LogWarning($"[{this.gameObject.name}] ItemImage not found, please check again!");
    }
    private void LoadItemNameText()
    {
        if (this.itemNameText != null) return;
        Debug.LogWarning($"[{this.gameObject.name}] ItemNameText not found, please check again!");
        this.itemNameText = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
    }
    private void LoadItemCountText()
    {
        if (this.itemCountText != null) return;
        Debug.LogWarning($"[{this.gameObject.name}] ItemCountText not found, please check again!");
        this.itemCountText = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
    }
    private void LoadAbleToSelect()
    {
        if (this.ableToSelect != null) return;
        this.ableToSelect = GetComponentInChildren<AbleToSelect>();
        Debug.LogWarning($"[{this.gameObject.name}] AbleToSelect not found, please check again!");
    }
    private void LoadDragItem()
    {
        if (this.dragItem != null) return;
        this.dragItem = GetComponent<DragItem>();
        Debug.LogWarning($"[{this.gameObject.name}] DragItem not found, please check again!");
    }
    public void SetItemName(string name)
    {
        this.ItemNameText.text = name;
    }
    public void SetItemCount(int count, int maxCount)
    {
        this.ItemCountText.text = $"{count}/{maxCount}";
    }
    public void SetItemSprite(Sprite sprite)
    {
        this.ItemImage.sprite = sprite;
    }
    public void SetAbleToSelectSkill(SkillType skillType)
    {
        if (this.AbleToSelect != null)
        {
            this.AbleToSelect.SkillType = skillType;
        }
    }
    public void SetAbleToSelectEquipment(ItemSO equipmentItem)
    {
        if (this.AbleToSelect != null)
        {
            this.AbleToSelect.EquipmentItem = equipmentItem;
        }
    }
    public void SetID(string id)
    {
        this.dragItem.SetID(id);
    }
    public void SetCurrentLevel(int level)
    {
        if (this.dragItem != null)
        {
            this.dragItem.SetCurrentLevel(level);
        }
    }
}
