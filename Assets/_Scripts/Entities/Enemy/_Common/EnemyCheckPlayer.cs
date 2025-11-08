
using UnityEngine;

public class EnemyCheckPlayer : MyMonoBehaviour
{
    public bool CheckNearPlayer(float distance)
    {
        float distanceToPlayer = Vector3.Distance(transform.parent.position, PlayerController.Instance.transform.position);
        return distanceToPlayer <= distance;
    }
}