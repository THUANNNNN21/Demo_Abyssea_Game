using UnityEngine;

public class LevelByDistance : Level
{
    [Header("Level By Distance")]
    [SerializeField] private Transform target;
    [SerializeField] private float distancePerLevel = 10f;
    [SerializeField] private float distance;
    protected virtual void FixedUpdate()
    {
        this.Leveling();
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void Leveling()
    {
        if (this.target == null) return;
        this.distance = Vector3.Distance(this.transform.position, this.target.position);
        int newLevel = this.GetlevelByDistance();
        this.SetLevel(newLevel);
    }
    private int GetlevelByDistance()
    {
        int level = Mathf.FloorToInt(this.distance / this.distancePerLevel) + 1;
        return level;
    }
}
