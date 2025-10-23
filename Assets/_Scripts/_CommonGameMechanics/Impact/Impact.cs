using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Impact : Cooldown
{
    [Header("Impact")]
    [SerializeField] private Collider2D col2D;
    [SerializeField] private Rigidbody2D rb2D;
    public Rigidbody2D Rb2D { get => rb2D; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigidbody();
    }
    private void LoadCollider()
    {
        if (this.col2D == null)
        {
            this.col2D = GetComponent<Collider2D>();
        }
        this.col2D.isTrigger = true;
    }
    private void LoadRigidbody()
    {
        if (this.rb2D == null)
        {
            this.rb2D = GetComponent<Rigidbody2D>();
        }
        this.rb2D.bodyType = RigidbodyType2D.Kinematic; // Không cần vật lý, chỉ cần trigger
        this.rb2D.gravityScale = 0; // Không cần trọng lực
    }
    protected abstract void OnTriggerEnter2D(Collider2D other);
    public void SetImpactRange(float range)
    {
        if (this.col2D is CircleCollider2D circleCol)
        {
            circleCol.radius = range;
        }
    }
}
