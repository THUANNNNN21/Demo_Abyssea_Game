using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIItem : MyMonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    public Image BackgroundImage => backgroundImage;
    [SerializeField] private Image itemImage;
    public Image ItemImage => itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    public TextMeshProUGUI ItemNameText => itemNameText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    public TextMeshProUGUI ItemCountText => itemCountText;
    protected override void LoadComponents()
    {
        this.LoadBackgroundImage();
        this.LoadItemImage();
        this.LoadItemNameText();
        this.LoadItemCountText();
    }
    private void LoadBackgroundImage()
    {
        if (this.backgroundImage != null) return;
        this.backgroundImage = transform.Find("Background").GetComponent<Image>();
        Debug.LogWarning($"[{this.gameObject.name}] BackgroundImage not found, please check again!");
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
    public void SetItemName(string name)
    {
        this.ItemNameText.text = name;
    }
    public void SetItemCount(int count)
    {
        this.ItemCountText.text = count.ToString();
    }
    public void SetItemSprite(Sprite sprite)
    {
        this.ItemImage.sprite = sprite;
    }
}
