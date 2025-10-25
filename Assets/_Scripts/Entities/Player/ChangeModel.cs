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
        Sprite baseMount = Resources.Load<Sprite>("Sprites/Mount/Mount0");
        Sprite baseBody = Resources.Load<Sprite>("Sprites/Body/Body0");
        Sprite baseWing = Resources.Load<Sprite>("Sprites/Wing/Wing7");
        this.ChangeMountSprite(baseMount);
        this.ChangeBodySprite(baseBody);
        this.ChangeWingSprite(baseWing);
    }
    private void LoadMountModel()
    {
        if (this.mountModel != null) return;
        else
        {
            this.mountModel = this.transform.parent.Find("Mount").GetComponent<SpriteRenderer>();
            Debug.LogWarning(this.gameObject.name + ": Load MountModel");
        }
    }
    private void LoadBodyModel()
    {
        if (this.bodyModel != null) return;
        else
        {
            this.bodyModel = this.mountModel.transform.Find("Body").GetComponent<SpriteRenderer>();
            Debug.LogWarning(this.gameObject.name + ": Load BodyModel");
        }
    }
    private void LoadLeftWingModel()
    {
        if (this.leftWingModel != null) return;
        else
        {
            this.leftWingModel = this.mountModel.transform.Find("WingLeft").GetComponent<SpriteRenderer>();
            Debug.LogWarning(this.gameObject.name + ": Load LeftWingModel");
        }
    }
    private void LoadRightWingModel()
    {
        if (this.rightWingModel != null) return;
        else
        {
            this.rightWingModel = this.mountModel.transform.Find("WingRight").GetComponent<SpriteRenderer>();
            Debug.LogWarning(this.gameObject.name + ": Load RightWingModel");
        }
    }
    public void ChangeMountSprite(Sprite newSprite)
    {
        this.mountModel.sprite = newSprite;
    }
    public void ChangeBodySprite(Sprite newSprite)
    {
        this.bodyModel.sprite = newSprite;
    }
    public void ChangeWingSprite(Sprite newSprite)
    {
        this.leftWingModel.sprite = newSprite;
        this.rightWingModel.sprite = newSprite;
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
