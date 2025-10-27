using UnityEngine;

public class DamageSender : MyMonoBehaviour
{
    #region Inspector Fields
    [Header("Damage Sender")]
    //[SerializeField] private DespawnByTrigger despawnByTrigger;
    [SerializeField] private int damage;
    #endregion

    #region Properties
    public int Damage
    {
        get { return this.damage; }
        set { this.damage = value; }
    }
    #endregion

    #region Public Methods
    public void SendDamage(Transform obj, int damage)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Remove(damage);
        damageReceiver.CheckDead();
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
    #endregion
}
