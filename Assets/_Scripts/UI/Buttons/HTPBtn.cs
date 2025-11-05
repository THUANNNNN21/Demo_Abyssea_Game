using UnityEngine;

public class HTPBtn : BaseButton
{
    protected override void OnClickButton()
    {
        MainMenuManager.Instance.ShowHowToPlay();
    }
}
