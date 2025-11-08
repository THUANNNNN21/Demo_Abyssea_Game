using UnityEngine;

public class MapLevel : LevelByTime
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.MapStartLeveling();
    }
    private void MapStartLeveling()
    {
        this.SetStartLeveling(true);
    }
    protected override void SetLevel(int newLevel)
    {
        base.SetLevel(newLevel);
        EnemySpawn.Instance.SetObjectsPerSpawn(this.CurrentLevel);
        float baseDelayTime = EnemySpawn.Instance.DelayTime;
        EnemySpawn.Instance.SetDelayTime(baseDelayTime - 0.5f);
    }
}
