using UnityEngine;

public class LevelByTime : Level
{
    [Header("Level By Time")]
    [SerializeField] private bool isStartLeveling = false;
    [SerializeField] private float timePerLevel = 10f;
    [SerializeField] private float time;
    protected virtual void FixedUpdate()
    {
        this.Leveling();
    }
    protected void SetStartLeveling(bool isStart)
    {
        this.isStartLeveling = isStart;
    }
    private void Leveling()
    {
        if (!this.isStartLeveling) return;
        this.time += Time.fixedDeltaTime;
        int newLevel = this.GetlevelByTime();
        this.SetLevel(newLevel);
    }
    private int GetlevelByTime()
    {
        int level = Mathf.FloorToInt(this.time / this.timePerLevel) + 1;
        return level;
    }
}
