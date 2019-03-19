
using UnityEngine;
public class Movement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private LayerMask groundType;

    private float moveSpeed = 3f;
    private float dirX;

    private readonly float groundRadius = 0.08f;
    private readonly float fallMultiplier = 1.5f;
    private readonly float lowJumpMultilier = 1f;

    private bool isGrounded;
    private bool facingRight;
  
    private void Start()
    {
        facingRight = true;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        Move();
        Flip();
    }

    private void Update()
    {
        HandleInput();

        if (m_rigidbody2D.velocity.y < 0)
            m_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        else if (m_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            m_rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultilier * Time.deltaTime;

        dirX = Input.GetAxis("Horizontal") * moveSpeed;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed = 6f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 3f;

        if (Input.GetButtonDown("Jump"))
            Jump();
      
    }

    private void Move()
    {
        m_rigidbody2D.velocity = new Vector2(dirX, m_rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        m_rigidbody2D.velocity = Vector2.up * 6f;
    }

    private bool IsGrounded()
    {
        if(m_rigidbody2D.velocity.y <= 0)
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundType);
                foreach(Collider2D collider in colliders)
                    if (collider.gameObject != gameObject)
                        return true;
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
}
