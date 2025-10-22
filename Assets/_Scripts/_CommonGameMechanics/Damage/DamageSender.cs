using UnityEngine;

public class DamageSender : MyMonoBehaviour
{
    [Header("Damage Sender")]
    //[SerializeField] private DespawnByTrigger despawnByTrigger;
    [SerializeField] private int damage;
    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }
    public void SendDamage(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Remove(this.damage);
        damageReceiver.CheckDead();
    }
    public void SetDamage(int newDamage)
    {
        this.Damage = newDamage;
    }
}
