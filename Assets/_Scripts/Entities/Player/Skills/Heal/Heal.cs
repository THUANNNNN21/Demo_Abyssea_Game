using UnityEngine;

public class Heal : MyMonoBehaviour
{
    [SerializeField] private HealController healController;
    public HealController HealController { get => healController; }
    [Header("Heal Settings")]
    [SerializeField] private int healDuration = 5; // Thời gian kéo dài heal (giây)
    [SerializeField] private float healPercentPerSecond = 5f; // % máu hồi mỗi giây

    private bool isHealing = false;
    private float healTimer = 0f; // Timer tổng thời gian heal
    private float tickTimer = 0f; // Timer cho mỗi lần hồi máu

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }

    private void LoadPlayerController()
    {
        if (this.healController == null)
        {
            this.healController = GetComponentInParent<HealController>();
        }
    }
    void FixedUpdate()
    {
        this.HealingProcess();
    }

    public void HealPlayer()
    {
        if (isHealing) return;

        isHealing = true;
        healTimer = 0f;
        tickTimer = 0f;

        Debug.Log($"Bắt đầu heal trong {healDuration} giây, hồi {healPercentPerSecond}% máu/giây");
    }

    private void HealingProcess()
    {
        int healMax = healController.SkillController.PlayerController.PlayerDamReceiver.HealthMax;
        if (!isHealing) return;

        healTimer += Time.fixedDeltaTime;
        tickTimer += Time.fixedDeltaTime;

        // Mỗi 1 giây hồi máu
        if (tickTimer >= 1f)
        {
            int healAmount = Mathf.RoundToInt(healMax * (healPercentPerSecond / 100f));
            healController.SkillController.PlayerController.PlayerDamReceiver.Add(healAmount);

            Debug.Log($"Hồi {healAmount} HP ({healPercentPerSecond}%). HP hiện tại: {healController.SkillController.PlayerController.PlayerDamReceiver.Health}/{healController.SkillController.PlayerController.PlayerDamReceiver.HealthMax}");

            tickTimer -= 1f;
        }

        // Kết thúc heal khi hết thời gian
        if (healTimer >= healDuration)
        {
            isHealing = false;
            healTimer = 0f;
            tickTimer = 0f;
            Debug.Log("Kết thúc heal");
            this.healController.ActiveHeal.StopHeal();
        }
    }
}
