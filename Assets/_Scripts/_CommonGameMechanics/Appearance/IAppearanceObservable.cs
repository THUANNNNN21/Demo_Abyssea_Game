using System;

public interface IAppearanceObservable
{
    event Action OnFullyAppeared;
    event Action OnAppearing;
    event Action OnDestroyed;
}