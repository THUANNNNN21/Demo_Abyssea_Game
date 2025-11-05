using UnityEngine;

public class QuitBtn : BaseButton
{
    protected override void OnClickButton()
    {
        MainMenuManager.Instance.QuitGame();
    }
}

