using UnityEngine;

public class EnemyLevelUp : MyMonoBehaviour
{
    [SerializeField] int scale;
    public int Scale { get => scale; }
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    private void LoadEnemyController()
    {
        if (this.enemyController == null)
        {
            this.enemyController = GetComponentInParent<EnemyController>();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
    }
    public void SetScale()
    {
        this.EnemyController.transform.localScale = Vector3.one * this.Scale;
    }
}