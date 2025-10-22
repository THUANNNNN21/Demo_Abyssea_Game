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
}
