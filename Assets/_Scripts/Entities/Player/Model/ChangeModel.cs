using UnityEngine;

public class ChangeModel : MyMonoBehaviour
{
    [SerializeField] private SpriteRenderer mountModel;
    public SpriteRenderer MountModel { get => mountModel; }
    [SerializeField] private SpriteRenderer bodyModel;
    public SpriteRenderer BodyModel { get => bodyModel; }
    [SerializeField] private SpriteRenderer leftWingModel;
    public SpriteRenderer LeftWingModel { get => leftWingModel; }
    [SerializeField] private SpriteRenderer rightWingModel;
    public SpriteRenderer RightWingModel { get => rightWingModel; }
    [SerializeField] private Sprite baseMount;
    [SerializeField] private Sprite baseBody;
    [SerializeField] private Sprite baseWing;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMountModel();
        this.LoadBodyModel();
        this.LoadLeftWingModel();
        this.LoadRightWingModel();
    }

    protected override void LoadValues()
    {
        base.LoadValues();
        this.LoadDefaultModel();
        this.ChangeMountSprite(baseMount);
        this.ChangeBodySprite(baseBody);
        this.ChangeWingSprite(baseWing);
    }
    private void LoadDefaultModel()
    {
        baseMount = Resources.Load<Sprite>("Sprites/Mount/Mount0");
        baseBody = Resources.Load<Sprite>("Sprites/Body/Body0");
        baseWing = Resources.Load<Sprite>("Sprites/Wing/Wing7");
    }

    private void LoadMountModel()
    {
        if (this.mountModel != null) return;
        this.mountModel = this.transform.parent.Find("Mount").GetComponent<SpriteRenderer>();
        Debug.LogWarning(this.gameObject.name + ": Load MountModel");
    }

    private void LoadBodyModel()
    {
        if (this.bodyModel != null) return;
        this.bodyModel = this.mountModel.transform.Find("Body").GetComponent<SpriteRenderer>();
        Debug.LogWarning(this.gameObject.name + ": Load BodyModel");
    }

    private void LoadLeftWingModel()
    {
        if (this.leftWingModel != null) return;
        this.leftWingModel = this.mountModel.transform.Find("WingLeft").GetComponent<SpriteRenderer>();
        Debug.LogWarning(this.gameObject.name + ": Load LeftWingModel");
    }

    private void LoadRightWingModel()
    {
        if (this.rightWingModel != null) return;
        this.rightWingModel = this.mountModel.transform.Find("WingRight").GetComponent<SpriteRenderer>();
        Debug.LogWarning(this.gameObject.name + ": Load RightWingModel");
    }

    public void ChangeMountSprite(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogWarning("[ChangeModel] Mount sprite is null!");
            return;
        }
        this.mountModel.sprite = newSprite;
        // Debug.Log($"[ChangeModel] Changed Mount to: {newSprite.name}");
    }

    public void ChangeBodySprite(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogWarning("[ChangeModel] Body sprite is null!");
            return;
        }
        this.bodyModel.sprite = newSprite;
        // Debug.Log($"[ChangeModel] Changed Body to: {newSprite.name}");
    }

    public void ChangeWingSprite(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogWarning("[ChangeModel] Wing sprite is null!");
            return;
        }
        this.leftWingModel.sprite = newSprite;
        this.rightWingModel.sprite = newSprite;
        // Debug.Log($"[ChangeModel] Changed Wing to: {newSprite.name}");
    }

    // NEW METHOD: Equip item dựa theo ItemModelType
    public void EquipItem(ItemSO itemSO)
    {
        if (itemSO == null)
        {
            Debug.LogError("[ChangeModel] ItemSO is null!");
            return;
        }

        if (itemSO.itemType != ItemType.Equipment)
        {
            Debug.LogWarning($"[ChangeModel] Item {itemSO.itemID} is not equipment!");
            return;
        }

        if (itemSO.sprite == null)
        {
            Debug.LogError($"[ChangeModel] Item {itemSO.itemID} has no sprite!");
            return;
        }

        switch (itemSO.itemModelType)
        {
            case ItemModelType.Mount:
                ChangeMountSprite(itemSO.sprite);
                // Debug.Log($"[ChangeModel] Equipped Mount: {itemSO.itemID}");
                break;

            case ItemModelType.Body:
                ChangeBodySprite(itemSO.sprite);
                // Debug.Log($"[ChangeModel] Equipped Body: {itemSO.itemID}");
                break;

            case ItemModelType.Wing:
                ChangeWingSprite(itemSO.sprite);
                // Debug.Log($"[ChangeModel] Equipped Wing: {itemSO.itemID}");
                break;

            default:
                Debug.LogWarning($"[ChangeModel] Unknown ItemModelType: {itemSO.itemModelType}");
                break;
        }
    }

    // NEW METHOD: Unequip và reset về default
    public void UnequipItem(ItemModelType modelType)
    {
        switch (modelType)
        {
            case ItemModelType.Mount:
                ChangeMountSprite(this.baseMount);
                // Debug.Log("[ChangeModel] Unequipped Mount");
                break;

            case ItemModelType.Body:
                ChangeBodySprite(this.baseBody);
                ChangeBodySprite(baseBody);
                // Debug.Log("[ChangeModel] Unequipped Body");
                break;

            case ItemModelType.Wing:
                ChangeWingSprite(this.baseWing);
                // Debug.Log("[ChangeModel] Unequipped Wing");
                break;

            default:
                Debug.LogWarning($"[ChangeModel] Cannot unequip: {modelType}");
                break;
        }
    }

    [Header("Test Change Model")]
    [SerializeField] private Sprite testMountSprite;
    [SerializeField] private Sprite testBodySprite;
    [SerializeField] private Sprite testWingSprite;

    [ContextMenu("Test Change Wing")]
    public void TestChangeWing()
    {
        this.testWingSprite = Resources.Load<Sprite>("Sprites/Wing/Wing1");
        this.ChangeWingSprite(testWingSprite);
    }

    [ContextMenu("Test Change Body")]
    public void TestChangeBody()
    {
        this.ChangeBodySprite(testBodySprite);
    }

    [ContextMenu("Test Change Mount")]
    public void TestChangeMount()
    {
        this.ChangeMountSprite(testMountSprite);
    }
}
