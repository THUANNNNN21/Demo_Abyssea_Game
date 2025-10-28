using UnityEngine;

public abstract class Warp : Cooldown
{
    protected Vector3 warpDirection;
    [SerializeField] protected float warpDistance = 5f;
    public void Warping(Transform obj)
    {
        Vector3 newPos = obj.position;
        newPos += warpDirection.normalized * this.warpDistance;
        obj.position = newPos;
        this.ResetCooldown();
    }
    protected abstract void CheckWarpPosition();
}