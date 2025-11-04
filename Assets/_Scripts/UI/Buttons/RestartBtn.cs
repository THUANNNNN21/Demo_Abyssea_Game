using UnityEngine;

public class RestartBtn : BaseButton
{
    protected override void OnClickButton()
    {
        GameManager.Instance.RestartGame();
    }
}
