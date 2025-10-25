using UnityEngine;

public class AbleToSelectBurn : AbleToSelect
{
    public override void OnSelect()
    {
        PlayerController.Instance.SkillController.BurnController.ActiveBurn.StartBurn();
    }
}