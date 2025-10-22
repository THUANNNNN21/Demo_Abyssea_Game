using UnityEngine;

public abstract class Cooldown : MyMonoBehaviour
{
    [Header("Cooldown Settings")]
    private float timer;
    [SerializeField] protected float delayTime;
    protected bool isReady = true;
    protected virtual void Timing()
    {
        if (isReady) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delayTime) return;
        this.isReady = true;
    }
    protected virtual void ResetCooldown()
    {
        this.isReady = false;
        this.timer = 0f;
    }
    protected void SetDelayTime(float delayTime)
    {
        this.delayTime = delayTime;
    }
}
