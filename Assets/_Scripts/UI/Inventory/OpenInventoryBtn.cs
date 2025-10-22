using UnityEngine;

public class OpenInventoryBtn : BaseButton
{
    protected override void OnClickButton()
    {
        UIInventory.Instance.Toggle();
    }
}
