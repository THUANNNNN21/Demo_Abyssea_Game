using System;
using UnityEngine;

[RequireComponent(typeof(IAppearanceObservable))]
public class AppearanceStateTracker : MyMonoBehaviour
{
    [Header("Appear States")]
    [SerializeField] private bool isAppearing = false;      // ✅ Biến 1: Đang appear
    [SerializeField] private bool hasAppeared = false;      // ✅ Biến 2: Đã appear xong

    // ✅ Public properties để đọc từ bên ngoài
    public bool IsAppearing => isAppearing;
    public bool HasAppeared => hasAppeared;

    protected IAppearanceObservable subject;

    protected override void Awake()
    {
        base.Awake();
        this.LoadAppearanceObservable();
        this.SubscribeToEvents();
    }

    private void LoadAppearanceObservable()
    {
        this.subject = GetComponent<IAppearanceObservable>();
    }

    // ✅ Đăng ký tất cả events cần thiết
    private void SubscribeToEvents()
    {
        if (subject != null)
        {
            subject.OnAppearing += OnAppearing;           // Khi bắt đầu appear
            subject.OnFullyAppeared += OnFullyAppeared;   // Khi appear xong
            subject.OnDestroyed += OnDestroyed;           // Khi destroyed
        }
    }

    // ✅ Event handlers
    private void OnAppearing()
    {
        isAppearing = true;
        hasAppeared = false;
    }

    private void OnFullyAppeared()
    {
        isAppearing = false;
        hasAppeared = true;
    }

    private void OnDestroyed()
    {
        isAppearing = false;
        hasAppeared = false;
    }

    private void OnDestroy()
    {
        if (subject != null)
        {
            subject.OnAppearing -= OnAppearing;
            subject.OnFullyAppeared -= OnFullyAppeared;
            subject.OnDestroyed -= OnDestroyed;
        }
    }
}