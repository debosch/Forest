using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected Animator animator;

    protected bool facingRight;

    protected readonly float groundCollisionRadius = 0.1f;
    protected readonly float jumpForce = 600f;
    protected readonly float moveSpeed = 7f;

    [SerializeField]
    protected Transform[] groundPoints;
    [SerializeField]
    protected LayerMask groundType;

    public Rigidbody2D RigidBody { get; set; }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        facingRight = true;
}

    void Update()
    {
        
    }

    protected void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
    }

    protected bool IsGrounded()
    {
        if (RigidBody.velocity.y <= 0)
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundCollisionRadius, groundType);
                foreach (Collider2D collider in colliders)
                    if (collider.gameObject != gameObject)
                    {
                        animator.SetBool("fall", false);
                        return true;
                    }

            }
        return false;
    }
}
