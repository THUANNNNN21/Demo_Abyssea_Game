using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PrefabSpawnRate
{
    public GameObject prefab;
    [Range(0, 100)]
    public float spawnRate = 33.33f; // Tỉ lệ spawn (%)

    [HideInInspector]
    public float cumulativeRate; // Tỉ lệ tích lũy (dùng cho random)
}

public class GetRandomPrefabs : MyMonoBehaviour
{
    [SerializeField] private List<PrefabSpawnRate> prefabSpawnRates = new();
    public List<PrefabSpawnRate> PrefabSpawnRates => prefabSpawnRates;

    [Header("Debug Info")]
    [SerializeField] private float totalRate;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.CalculateCumulativeRates();
    }

    private void OnValidate()
    {
        this.CalculateCumulativeRates();
    }

    /// <summary>
    /// Tính toán tỉ lệ tích lũy cho mỗi prefab.
    /// Ví dụ: 70%, 20%, 10% => cumulative: 70, 90, 100
    /// </summary>
    private void CalculateCumulativeRates()
    {
        totalRate = 0f;

        foreach (var item in prefabSpawnRates)
        {
            totalRate += item.spawnRate;
            item.cumulativeRate = totalRate;
        }
    }

    /// <summary>
    /// Lấy random prefab dựa trên tỉ lệ spawn.
    /// Random từ 0-100, so sánh với tỉ lệ tích lũy để chọn prefab.
    /// </summary>
    public GameObject GetRandomPrefab()
    {
        if (prefabSpawnRates.Count == 0)
        {
            return null;
        }

        // Random một số từ 0 đến tổng tỉ lệ
        float randomValue = Random.Range(0f, totalRate);

        // Tìm prefab tương ứng với random value
        foreach (var item in prefabSpawnRates)
        {
            if (randomValue <= item.cumulativeRate)
            {
                return item.prefab;
            }
        }

        // Fallback: trả về prefab cuối cùng
        return prefabSpawnRates[prefabSpawnRates.Count - 1].prefab;
    }
}
