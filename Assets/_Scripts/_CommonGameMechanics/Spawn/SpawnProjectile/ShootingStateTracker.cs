using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IShootingObservable))]
public class ShootingStateTracker : MyMonoBehaviour
{
    [Header("Shooting States")]
    [SerializeField] private bool isShooting = false;
    public bool IsShooting => isShooting;

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
            subject.OnShootComplete += OnShootComplete;
        }
    }
    private void OnShooting()
    {
        isShooting = true;
    }

    private void OnShootComplete()
    {
        isShooting = false;
    }
}