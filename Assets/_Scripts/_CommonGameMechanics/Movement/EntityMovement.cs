using UnityEngine;

public abstract class EntityMovement : MyMonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float speed = 10f;
    protected Vector3 direction;
    [SerializeField] public bool initialized = false;
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}