using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MyMonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] protected Button button;
    void Start()
    {
        this.AddOnClickEvent();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }
    protected virtual void LoadButton()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.LogWarning("LoadButton: " + this.button, this);
    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(OnClickButton);
    }
    protected abstract void OnClickButton();
}
