using UnityEngine;

public class MainMenuBtn : BaseButton
{
    protected override void OnClickButton()
    {
        GameManager.Instance.QuitToMainMenu();
    }
}
