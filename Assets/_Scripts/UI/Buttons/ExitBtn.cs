using UnityEditor;
using UnityEngine;

public class ExitBtn : BaseButton
{
    protected override void OnClickButton()
    {
        Debug.Log("Exit button clicked");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();

    }
}
