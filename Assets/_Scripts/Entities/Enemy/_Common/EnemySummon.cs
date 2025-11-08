using UnityEngine;
public class EnemySummon : EnemyShooting
{
    protected override void SpawnWithCooldown()
    {
        if (this.isReady)
        {
            this.PlayShootAnimation();
            GameObject enemy = this.SpawnAndReturn();
            if (enemy != null)
            {
                EnemyController enemyCtrl = enemy.GetComponent<EnemyController>();
                HPBarSpawner.Instance.SpawnHPBar(enemyCtrl);
                this.ResetCooldown();
            }
        }
    }
}