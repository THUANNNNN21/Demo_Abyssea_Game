using UnityEngine;

public class AbleToSelectWarp : AbleToSelect
{
    public override void OnSelect()
    {
        PlayerController.Instance.Warp.StartWarp();
        Debug.Log("Warp skill activated via hotkey.", this);
    }
}