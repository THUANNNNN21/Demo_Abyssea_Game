using UnityEngine;

public enum SkillType
{
    None,
    Shooting,
    Burn,
    Heal,
    Warp
}

public class AbleToSelect : MonoBehaviour
{
    [SerializeField] private SkillType skillType = SkillType.Shooting;
    public SkillType SkillType
    {
        get => skillType;
        set => skillType = value;
    }

    public virtual void OnSelect()
    {
        SkillController skillController = PlayerController.Instance.SkillController;

        switch (skillType)
        {
            case SkillType.Shooting:
                skillController.PlayerShooting.SpawnWithCooldown();
                break;

            case SkillType.Burn:
                skillController.BurnController.ActiveBurn.StartBurn();
                break;

            case SkillType.Heal:
                skillController.HealController.ActiveHeal.StartHeal();
                break;

            case SkillType.Warp:
                skillController.Warp.StartWarp();
                break;

            default:
                Debug.LogWarning($"Unknown skill type: {skillType}");
                break;
        }
    }
}