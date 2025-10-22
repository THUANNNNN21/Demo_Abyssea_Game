using TMPro;
using UnityEngine;

public class BaseText : MyMonoBehaviour
{
    [Header("Base Text Settings")]
    [SerializeField] protected TextMeshProUGUI uiText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIText();
    }
    private void LoadUIText()
    {
        if (uiText != null) return;
        uiText = GetComponent<TextMeshProUGUI>();
        Debug.LogWarning("Load UIText: " + uiText.name, gameObject);
    }
}
