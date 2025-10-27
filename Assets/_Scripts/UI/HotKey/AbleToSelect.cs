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
    [Header("Skill Settings")]
    [SerializeField] private SkillType skillType = SkillType.Shooting;
    public SkillType SkillType
    {
        get => skillType;
        set => skillType = value;
    }

    [Header("Equipment Settings (Optional)")]
    [SerializeField] private ItemSO equipmentItem;
    public ItemSO EquipmentItem
    {
        get => equipmentItem;
        set => equipmentItem = value;
    }

    // ✅ Method mới: Chỉ equip item khi select
    public void EquipItem()
    {
        if (equipmentItem != null && equipmentItem.itemType == ItemType.Equipment)
        {
            PlayerController playerController = PlayerController.Instance;

            if (playerController == null)
            {
                Debug.LogError("[AbleToSelect] PlayerController.Instance is null!");
                return;
            }

            HandleEquipment(playerController.ChangeModel);
        }
    }

    // ✅ Method mới: Chỉ cast skill khi click chuột
    public void CastSkill()
    {
        PlayerController playerController = PlayerController.Instance;

        if (playerController == null)
        {
            Debug.LogError("[AbleToSelect] PlayerController.Instance is null!");
            return;
        }

        HandleSkill(playerController.SkillController);
    }

    // ⚠️ Giữ lại OnSelect() cho backward compatibility (nếu có code cũ gọi)
    public virtual void OnSelect()
    {
        EquipItem(); // Đổi equipment
        CastSkill(); // Cast skill
    }

    private void HandleSkill(SkillController skillController)
    {
        if (skillController == null)
        {
            Debug.LogError("[AbleToSelect] SkillController is null!");
            return;
        }

        switch (skillType)
        {
            case SkillType.Shooting:
                skillController.PlayerShooting.SpawnWithCooldown();
                Debug.Log("[AbleToSelect] Activated Shooting");
                break;

            case SkillType.Burn:
                skillController.BurnController.ActiveBurn.StartBurn();
                Debug.Log("[AbleToSelect] Activated Burn");
                break;

            case SkillType.Heal:
                skillController.HealController.ActiveHeal.StartHeal();
                Debug.Log("[AbleToSelect] Activated Heal");
                break;

            case SkillType.Warp:
                skillController.Warp.StartWarp();
                Debug.Log("[AbleToSelect] Activated Warp");
                break;

            case SkillType.None:
                Debug.LogWarning("[AbleToSelect] No skill assigned!");
                break;

            default:
                Debug.LogWarning($"[AbleToSelect] Unknown skill type: {skillType}");
                break;
        }
    }

    private void HandleEquipment(ChangeModel changeModel)
    {
        if (changeModel == null)
        {
            Debug.LogError("[AbleToSelect] ChangeModel is null!");
            return;
        }

        changeModel.EquipItem(equipmentItem);
    }

    // Helper methods
    public void SetEquipment(ItemSO item)
    {
        if (item != null && item.itemType == ItemType.Equipment)
        {
            this.equipmentItem = item;
            Debug.Log($"[AbleToSelect] Equipment set to: {item.itemID}");
        }
        else
        {
            Debug.LogWarning("[AbleToSelect] Item is not equipment!");
        }
    }

    public void RemoveEquipment()
    {
        this.equipmentItem = null;
        Debug.Log("[AbleToSelect] Equipment removed");
    }

    public bool HasEquipment()
    {
        return equipmentItem != null && equipmentItem.itemType == ItemType.Equipment;
    }
}