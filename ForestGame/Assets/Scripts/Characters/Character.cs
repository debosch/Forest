using UnityEngine;

public abstract class Character : MonoBehaviour
{

    protected Animator animator;

    protected bool facingRight;


    protected readonly float groundCollisionRadius = 0.1f;
    protected readonly float jumpForce = 700f;
    protected readonly float moveSpeed = 7f;

    [SerializeField]
    protected Transform[] groundPoints;
    [SerializeField]
    protected LayerMask groundType;

    public Rigidbody2D RigidBody { get; set; }

    private readonly float minX = -18.2f, maxY = 9.15f;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    protected void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
    }

    protected void Jump()
    {
        RigidBody.AddForce(new Vector2(0, jumpForce));
        animator.SetTrigger("jump");
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
                        return true;
                    }

            }
        return false;
    }

    protected void HandleBoundary()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minX, transform.position.x),
            Mathf.Clamp(transform.position.y, transform.position.y, maxY));
    }
}
