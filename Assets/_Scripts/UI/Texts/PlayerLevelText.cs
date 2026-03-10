using UnityEngine;

public class PlayerLevelText : BaseText
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerController();
    }
    private void LoadPlayerController()
    {
        if (playerController != null) return;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.LogWarning("Load PlayerController: " + playerController.name, gameObject);
    }
    void FixedUpdate()
    {
        this.UpdateLevelText();
    }
    private void UpdateLevelText()
    {
        string level = this.playerController.LevelController.LevelUp.CurrentLevel.ToString();
        this.uiText.text = "Lv. " + level + "       Exp:" + this.playerController.LevelController.LevelUp.CurrentExp.ToString() + "/" + this.playerController.LevelController.LevelUp.ExpToNextLevel.ToString();
    }
}
