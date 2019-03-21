
using UnityEngine;
public class Player : Character
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    public bool OnGround { get; set; }
    public bool Jumping { get; set; }
    public bool Attack { get; set; }
    public bool Walk { get; set; }
    public bool Dead { get; set; }

    private float dirX;

    private readonly float fallMultiplier = 1.5f;
    private readonly float lowJumpMultilier = 1f;

    override protected void Start()
    {
        base.Start();
        Dead = false;
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

            if (RigidBody.velocity.y < 0)
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            else if (RigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultilier * Time.deltaTime;

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

        if (RigidBody.velocity.y < 0)
            animator.SetBool("fall",true);

        if(Jumping && OnGround)
        {
            RigidBody.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("jump");
        }
          
        if(!Attack)
            RigidBody.velocity = new Vector2(dirX, RigidBody.velocity.y);

    }

    

    private void Flip()
    {
        if(dirX > 0 && !facingRight || dirX < 0 && facingRight)
        {
            ChangeDirection();
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
