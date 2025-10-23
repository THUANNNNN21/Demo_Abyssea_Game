using UnityEngine;

public class AbleToSelectShooting : AbleToSelect
{
    public override void OnSelect()
    {
        PlayerController.Instance.PlayerShooting.SpawnWithCooldown();
        Debug.Log("Shooting skill activated via hotkey.", this);
    }
}