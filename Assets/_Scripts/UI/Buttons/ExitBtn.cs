using UnityEditor;
using UnityEngine;

public class ExitBtn : BaseButton
{
    protected override void OnClickButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();

    }
}
