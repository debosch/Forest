
using UnityEngine;
public class Movement : MonoBehaviour
{
    private static Movement instance;
    public static Movement Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Movement>();
            return instance;
        }
    }

    public Rigidbody2D M_RigidBody2D { get; set; }
    public bool OnGround { get; set; }
    public bool Jumping { get; set; }
    public bool Attack { get; set; }
    public bool Walk { get; set; }
    public bool Fall { get; set; }
    public bool Dead { get; set; }


    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private LayerMask groundType;

    private Animator animator;

    private float dirX;

    private readonly float moveSpeed = 7f;
    private readonly float groundCollisionRadius = 0.1f;
    private readonly float fallMultiplier = 1.5f;
    private readonly float lowJumpMultilier = 1f;
    private readonly float jumpForce = 600f;


    private bool facingRight;

    private void Start()
    { 
        Dead = false;
        facingRight = true;
        animator = GetComponent<Animator>();
        M_RigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        OnGround = IsGrounded();
        HandleMovement();
        HandleLayers();
        if(!Attack)
            Flip();
    }

    private void Update()
    {
        if(!Dead)
        {
            HandleInput();

            if (M_RigidBody2D.velocity.y < 0)
                M_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            else if (M_RigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))
                M_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultilier * Time.deltaTime;

            dirX = Input.GetAxis("Horizontal") * moveSpeed;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Attack)
            animator.SetTrigger("jump");
        
        if (Input.GetKey(KeyCode.LeftShift) && OnGround && !Attack)
            animator.SetTrigger("attack");
    }

    private void HandleMovement()
    {
        animator.SetFloat("speed", Mathf.Abs(dirX));

        if (M_RigidBody2D.velocity.y < 0)
            animator.SetBool("fall",true);

        if(Jumping && OnGround)
        {
            M_RigidBody2D.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("jump");
        }
          
        if(!Attack)
            M_RigidBody2D.velocity = new Vector2(dirX, M_RigidBody2D.velocity.y);

    }

    private bool IsGrounded()
    {
        if(M_RigidBody2D.velocity.y <= 0)
            foreach(Transform point in groundPoints)
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

    private void Flip()
    {
        if(dirX > 0 && !facingRight || dirX < 0 && facingRight)
        {
            facingRight = !facingRight;

            var theScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);

            transform.localScale = theScale;
        }
    }

    private void HandleLayers()
    {
        if (!OnGround)
            animator.SetLayerWeight(1, 1);
        else
            animator.SetLayerWeight(1, 0);
    }
}
