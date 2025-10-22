using System;

public interface IEnemyDespawnObservable
{
    event Action<EnemyController> OnEnemyDeath;
}