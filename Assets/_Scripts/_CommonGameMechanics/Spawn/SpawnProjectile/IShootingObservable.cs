using System;

public interface IShootingObservable
{
    event Action OnShooting;
    event Action OnStopShooting;
}