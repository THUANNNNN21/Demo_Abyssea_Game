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
        this.button.onClick.AddListener(PlayClickSound);
    }
    protected abstract void OnClickButton();
    private void PlayClickSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick, 1f);
        }
    }
}
