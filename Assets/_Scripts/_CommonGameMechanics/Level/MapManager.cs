using UnityEngine;

public class MapManager : MyMonoBehaviour
{
    private static MapManager instance;
    public static MapManager Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    [SerializeField] private MapLevel mapLevel;
    public MapLevel MapLevel => mapLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapLevel();
    }
    private void LoadMapLevel()
    {
        if (mapLevel != null) return;
        mapLevel = GetComponentInChildren<MapLevel>();
        Debug.LogWarning("Load MapLevel: " + mapLevel.name, gameObject);
    }
}
