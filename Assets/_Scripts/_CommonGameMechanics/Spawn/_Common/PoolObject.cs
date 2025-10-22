using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MyMonoBehaviour
{
    [Header("Object Pool")]
    public List<Transform> pool = new();
}