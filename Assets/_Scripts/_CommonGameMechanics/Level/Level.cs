using UnityEngine;

public class Level : MyMonoBehaviour
{
    [Header("Level")]
    [SerializeField] private int currentLevel = 1;
    public int CurrentLevel { get => currentLevel; }
    [SerializeField] private int maxLevel = 10;
    public int MaxLevel { get => maxLevel; }
    protected void LevelUp()
    {
        this.currentLevel += 1;
        this.LimitLevel();
    }
    protected void SetLevel(int level)
    {
        this.currentLevel = level;
        this.LimitLevel();
    }
    protected void LevelDown()
    {
        this.currentLevel -= 1;
        this.LimitLevel();
    }
    protected void LimitLevel()
    {
        if (this.currentLevel > this.maxLevel)
        {
            this.currentLevel = this.maxLevel;
        }
        if (this.currentLevel < 1)
        {
            this.currentLevel = 1;
        }
    }
}
