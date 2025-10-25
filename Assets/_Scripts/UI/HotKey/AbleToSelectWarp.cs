using UnityEngine;

public class AbleToSelectWarp : AbleToSelect
{
    public override void OnSelect()
    {
        PlayerController.Instance.SkillController.Warp.StartWarp();
    }
}