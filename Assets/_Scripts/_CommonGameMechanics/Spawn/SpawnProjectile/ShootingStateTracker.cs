using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IShootingObservable))]
public class ShootingStateTracker : MyMonoBehaviour
{
    [Header("Shooting States")]
    [SerializeField] private bool isShooting = false;
    [SerializeField] private bool hasStoppedShooting = false;
    public bool IsShooting => isShooting;
    public bool HasStoppedShooting => hasStoppedShooting;

    protected IShootingObservable subject;

    protected override void Awake()
    {
        base.Awake();
        this.LoadShootingObservable();
        this.SubscribeToEvents();
    }

    private void LoadShootingObservable()
    {
        this.subject = GetComponent<IShootingObservable>();
    }
    private void SubscribeToEvents()
    {
        if (subject != null)
        {
            subject.OnShooting += OnShooting;
            subject.OnStopShooting += OnStopShooting;
        }
    }
    private void OnShooting()
    {
        isShooting = true;
        hasStoppedShooting = false;
    }

    private void OnStopShooting()
    {
        isShooting = false;
        hasStoppedShooting = true;
    }
}