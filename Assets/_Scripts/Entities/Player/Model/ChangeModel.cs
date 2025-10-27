using UnityEngine;

public class ChangeModel : MyMonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private SpriteRenderer mountModel;
    [SerializeField] private SpriteRenderer bodyModel;
    [SerializeField] private SpriteRenderer leftWingModel;
    [SerializeField] private SpriteRenderer rightWingModel;
    [SerializeField] private Sprite baseMount;
    [SerializeField] private Sprite baseBody;
    [SerializeField] private Sprite baseWing;

    // [Header("Test Change Model")]
    // [SerializeField] private Sprite testMountSprite;
    // [SerializeField] private Sprite testBodySprite;
    // [SerializeField] private Sprite testWingSprite;
    #endregion

    #region Properties
    public SpriteRenderer MountModel { get => mountModel; }
    public SpriteRenderer BodyModel { get => bodyModel; }
    public SpriteRenderer LeftWingModel { get => leftWingModel; }
    public SpriteRenderer RightWingModel { get => rightWingModel; }
    #endregion

    #region Unity Methods
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
    #endregion

    #region Load Methods
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
    #endregion

    #region Change Sprite Methods
    public void ChangeMountSprite(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogWarning("[ChangeModel] Mount sprite is null!");
            return;
        }
        this.mountModel.sprite = newSprite;
    }

    public void ChangeBodySprite(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogWarning("[ChangeModel] Body sprite is null!");
            return;
        }
        this.bodyModel.sprite = newSprite;
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
    }
    #endregion

    #region Equip/Unequip Methods
    public void EquipItem(ItemSO itemSO)
    {
        if (itemSO == null)
        {
            Debug.LogError("[ChangeModel] ItemSO is null!");
            return;
        }

        if (itemSO.itemType != ItemType.Equipment)
        {
            Debug.LogWarning($"[ChangeModel] Item {itemSO.itemName} is not equipment!");
            return;
        }

        if (itemSO.sprite == null)
        {
            Debug.LogError($"[ChangeModel] Item {itemSO.itemName} has no sprite!");
            return;
        }

        switch (itemSO.itemModelType)
        {
            case ItemModelType.Mount:
                ChangeMountSprite(itemSO.sprite);
                break;

            case ItemModelType.Body:
                ChangeBodySprite(itemSO.sprite);
                break;

            case ItemModelType.Wing:
                ChangeWingSprite(itemSO.sprite);
                break;

            default:
                Debug.LogWarning($"[ChangeModel] Unknown ItemModelType: {itemSO.itemModelType}");
                break;
        }
    }

    public void UnequipItem(ItemModelType modelType)
    {
        switch (modelType)
        {
            case ItemModelType.Mount:
                ChangeMountSprite(this.baseMount);
                break;

            case ItemModelType.Body:
                ChangeBodySprite(this.baseBody);
                break;

            case ItemModelType.Wing:
                ChangeWingSprite(this.baseWing);
                break;

            default:
                Debug.LogWarning($"[ChangeModel] Cannot unequip: {modelType}");
                break;
        }
    }
    #endregion

    #region Test Methods
    // [ContextMenu("Test Change Wing")]
    // public void TestChangeWing()
    // {
    //     this.testWingSprite = Resources.Load<Sprite>("Sprites/Wing/Wing1");
    //     this.ChangeWingSprite(testWingSprite);
    // }

    // [ContextMenu("Test Change Body")]
    // public void TestChangeBody()
    // {
    //     this.ChangeBodySprite(testBodySprite);
    // }

    // [ContextMenu("Test Change Mount")]
    // public void TestChangeMount()
    // {
    //     this.ChangeMountSprite(testMountSprite);
    // }
    #endregion
}
