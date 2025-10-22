using System.Collections.Generic;
using UnityEngine;

public class GetRandomPoints : MyMonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new();
    public List<Transform> SpawnPoints => spawnPoints;
    private void LoadListSpawnPoints()
    {
        if (spawnPoints.Count == 0)
        {
            GetListSpawnPoints();
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListSpawnPoints();
    }
    public void GetListSpawnPoints()
    {
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("SpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }
    }
    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
}
