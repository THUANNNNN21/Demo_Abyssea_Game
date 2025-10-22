using Unity.VisualScripting;
using UnityEngine;

public class MeteoriteRotate : MyMonoBehaviour
{
    [SerializeField] protected EnemyController controller;
    public EnemyController Controller { get => controller; }
    protected override void LoadComponents()
    {
        this.LoadController();
    }
    private void LoadController()
    {
        if (controller == null)
        {
            controller = GetComponentInParent<EnemyController>();
        }
    }
    protected float rotationSpeed = 90f; // Default rotation speed
    protected virtual void FixedUpdate()
    {
        RotateMeteorite();
    }
    private void RotateMeteorite()
    {
        this.Controller.Model.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime, Space.Self);
    }
}
