
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

    public int HealthPoints { get; set; }
    
    public bool OnGround { get; set; }
    public bool Jumping { get; set; }
    public bool Attack { get; set; }
    public bool Walk { get; set; }
    public bool Dead { get; set; }
    public bool Falling { get; set; }


    private float dirX;

    private readonly float fallMultiplier = 1.3f;
    private readonly float lowJumpMultilier = 2f;

    [SerializeField]
    private Transform startPos;

    override protected void Start()
    {
        base.Start();
        animator.SetBool("dead", false);
        Dead = false;
        transform.position = startPos.position;
    }

    private void Awake()
    {
        HealthPoints = 3;
    }

    private void FixedUpdate()
    {
        if(!Dead)
        {
            OnGround = IsGrounded();
            HandleMovement();
            if(!Attack)
               Flip();
        }
        HandleLayers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            HealthPoints--;

        if (HealthPoints <= 0)
        {
            animator.SetBool("dead", true);
        }        

        if(!Dead)
        {
            HandleInput();

            if (RigidBody.velocity.y < 0)
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            else if (RigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultilier * Time.deltaTime;

            dirX = Input.GetAxis("Horizontal") * moveSpeed;
        }

        if (OnGround)
        {
            Falling = false;
            animator.SetBool("fall", false);
        }

        HandleBoundary();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Attack && OnGround && !Falling)
        {
            Jump();
            animator.SetTrigger("jump");
        }
            


        if (Input.GetKey(KeyCode.LeftShift) && OnGround && !Attack)
            animator.SetTrigger("attack");
    }

    private void HandleMovement()
    {
        animator.SetFloat("speed", Mathf.Abs(dirX));

        if (RigidBody.velocity.y < 0)
        {
            animator.SetBool("fall", true);   
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
