using UnityEngine;

public class DespawnByDistance : DespawnObject
{
    [Header("Distance Settings")]
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 endPosition;
    [SerializeField] private float maxDistance;

    [SerializeField] private float checkInterval = 0.2f; // kiểm tra mỗi 0.2 giây

    private void OnEnable()
    {
        this.startPosition = this.transform.position;
        InvokeRepeating(nameof(Despawn), checkInterval, checkInterval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Despawn));
    }

    public override void Despawn()
    {
        this.SetEndPosition(this.transform.position);
        if (Vector3.Distance(this.startPosition, this.endPosition) > maxDistance)
        {
            // Assuming you have a method to handle despawning
            this.HandleDespawn();
        }
    }

    public void SetDistance(float distance)
    {
        this.maxDistance = distance;
    }
    protected virtual void SetEndPosition(Vector3 position)
    {
        this.endPosition = position;
    }
}