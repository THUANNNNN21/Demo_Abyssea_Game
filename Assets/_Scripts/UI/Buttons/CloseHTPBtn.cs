public class NextHTPBtn : BaseButton
{
    protected override void OnClickButton()
    {
        MainMenuManager.Instance.CloseHowToPlay();
    }
}