using UnityEngine;

public class StartGameBtn : BaseButton
{
    protected override void OnClickButton()
    {
        MainMenuManager.Instance.StartGame();
    }
}
