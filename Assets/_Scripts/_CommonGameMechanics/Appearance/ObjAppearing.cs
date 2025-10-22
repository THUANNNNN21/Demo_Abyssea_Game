using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjAppearing : MyMonoBehaviour, IAppearanceObservable
{
    [Header("Appearance Settings")]
    [SerializeField] private EnemyController enemyController;
    public EnemyController EnemyController { get => enemyController; }
    [SerializeField] private Animator animator;

    // ✅ Implement Interface events
    public event Action OnFullyAppeared;
    public event Action OnAppearing;
    public event Action OnDestroyed;

    private void LoadEnemyController()
    {
        if (this.enemyController == null)
        {
            this.enemyController = transform.parent.GetComponentInParent<EnemyController>();
        }
    }
    private void LoadAnimator()
    {
        this.animator = this.enemyController.Animator;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyController();
        this.LoadAnimator();
    }

    // ✅ Protected methods để class con có thể trigger events
    protected virtual void TriggerOnAppearing()
    {
        OnAppearing?.Invoke();
    }

    protected virtual void TriggerOnFullyAppeared()
    {
        OnFullyAppeared?.Invoke();
    }

    protected virtual void TriggerOnDestroyed()
    {
        OnDestroyed?.Invoke();
    }
    // Virtual method cho class con có thể override, logic khi fully appeared, thông báo sự kiện fully appeared
    public virtual void FullyAppeared()
    {
        if (animator != null)
        {
            animator.SetTrigger("hasAppeared");
        }
        TriggerOnFullyAppeared();
    }
    public virtual void DestroyObject()
    {
        TriggerOnDestroyed();
    }
    private void Start()
    {
        this.TriggerOnAppearing();
    }
}
