using UnityEditor;
using UnityEngine;

public class ExitBtn : BaseButton
{
    protected override void OnClickButton()
    {
        GameManager.Instance.QuitToMainMenu();
    }
}
