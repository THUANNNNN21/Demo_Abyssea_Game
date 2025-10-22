using UnityEngine;

public class ProjectileAnimmationEvent : MyMonoBehaviour
{
    [SerializeField] private ProjectileController projectileController;
    public ProjectileController ProjectileController { get => projectileController; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectileController();
    }
    private void LoadProjectileController()
    {
        if (this.projectileController != null) return;
        else
        {
            this.projectileController = this.GetComponentInParent<ProjectileController>();
        }
    }
    public void OnDeathAnimationComplete()
    {
        this.projectileController.DespawnByDistance.HandleDespawn();
    }
    // public void OnAppearAnimationComplete()
    // {
    //     this.projectileController.ObjAppearing.FullyAppeared();
    // }
    // public void OnShootAnimationComplete()
    // {
    //     for (int i = 0; i < this.projectileController.EnemyShooting.Count; i++)
    //     {
    //         var shooting = this.projectileController.EnemyShooting[i];
    //         shooting.TriggerOnStopShooting();
    //     }
    // }
}
