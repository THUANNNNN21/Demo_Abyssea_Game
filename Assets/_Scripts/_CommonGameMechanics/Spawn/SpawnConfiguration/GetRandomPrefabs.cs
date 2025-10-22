using UnityEngine;
using System.Collections.Generic;

public class GetRandomPrefabs : MyMonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new();
    public List<GameObject> Prefabs => prefabs;
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public GameObject GetRandomPrefab()
    {
        if (prefabs.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, prefabs.Count);
        return prefabs[randomIndex];
    }
}
