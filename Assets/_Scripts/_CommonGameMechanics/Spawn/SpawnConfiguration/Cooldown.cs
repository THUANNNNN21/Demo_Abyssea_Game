using UnityEngine;

public abstract class Cooldown : MyMonoBehaviour
{
    [Header("Cooldown Settings")]
    [SerializeField] private float timer;
    [SerializeField] protected float delayTime;
    public float DelayTime { get => delayTime; }
    [SerializeField] protected bool isReady = true;
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
    public void SetDelayTime(float delayTime)
    {
        this.delayTime = delayTime;
    }
}
