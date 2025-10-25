using UnityEngine;

public class AbleToSelectShooting : AbleToSelect
{
    public override void OnSelect()
    {
        PlayerController.Instance.SkillController.PlayerShooting.SpawnWithCooldown();
    }
}