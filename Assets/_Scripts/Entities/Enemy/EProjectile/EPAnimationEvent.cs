using UnityEngine;

public class EPAnimation : MyMonoBehaviour
{
    [SerializeField] EProjectileCtrl eProjectileCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEProjectileCtrl();
    }
    private void LoadEProjectileCtrl()
    {
        if (this.eProjectileCtrl != null) return;
        this.eProjectileCtrl = this.GetComponentInParent<EProjectileCtrl>();
        Debug.Log("Load EProjectileCtrl " + this.eProjectileCtrl.name + " in " + this.name);
    }
    public void OnDestructionAnimationComplete()
    {
        this.eProjectileCtrl.DespawnByDistance.HandleDespawn();
    }
}
