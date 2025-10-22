using UnityEngine;

public class ExitInventoryBtn : BaseButton
{
    protected override void OnClickButton()
    {
        UIInventory.Instance.Close();
    }
}
