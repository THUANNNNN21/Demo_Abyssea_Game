using UnityEngine;
public class EnemySummon : EnemyShooting
{
    protected override void SpawnWithCooldown()
    {
        if (this.isReady)
        {
            GameObject enemy = this.SpawnAndReturn();
            // enemy.GetComponent<EnemyController>().Movement.SpawnPoint = this.transform.parent;
            this.ResetCooldown();
        }
    }
}