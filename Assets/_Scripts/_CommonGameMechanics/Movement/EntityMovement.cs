using UnityEngine;

public abstract class EntityMovement : MyMonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float speed = 10f;
    protected Vector3 direction;
    protected bool isMoving = true;
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetIsMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }
}