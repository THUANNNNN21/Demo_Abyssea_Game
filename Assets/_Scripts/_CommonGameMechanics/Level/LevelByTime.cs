using UnityEngine;

public class LevelByTime : Level
{
    [Header("Level By Time")]
    [SerializeField] private bool isStartLeveling = false;
    [SerializeField] private float timePerLevel = 30f;
    [SerializeField] private float time;
    void Start()
    {
        this.time = 0f;
        Invoke(nameof(Leveling), timePerLevel);
    }
    protected virtual void FixedUpdate()
    {
        this.time += Time.fixedDeltaTime;
    }
    protected void SetStartLeveling(bool isStart)
    {
        this.isStartLeveling = isStart;
    }
    private void Leveling()
    {
        if (!this.isStartLeveling) return;
        int newLevel = this.GetlevelByTime();
        if (newLevel != this.CurrentLevel)
        {
            this.SetLevel(newLevel);
        }
    }
    private int GetlevelByTime()
    {
        int level = Mathf.FloorToInt(this.time / this.timePerLevel) + 1;
        return level;
    }
}
