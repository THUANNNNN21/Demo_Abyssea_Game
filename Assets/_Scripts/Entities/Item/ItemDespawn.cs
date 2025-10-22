using UnityEngine;

public class ItemDespawn : DespawnByDistance
{
    [SerializeField] private GameObject player;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected override void LoadValues()
    {
        this.SetDistance(60f);
    }
    private void LoadPlayer()
    {
        if (this.player != null) return;
        else
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    protected override void SetEndPosition(Vector3 position)
    {
        this.endPosition = player.transform.position;
    }
}
