using UnityEngine;
using System.Collections;

public class GreenMonster : Character
{
    [Range(0, 3)]
    public float speed;

    [SerializeField]
    private Transform leftEdge, rightEdge;

    [SerializeField]
    private Collider2D enemyCollider;
    private Collider2D playerCollider;

    private Vector2 dirX;

    override protected void Start()
    {
        base.Start();

    }

    private void Awake()
    {
        dirX = Vector2.right;
        facingRight = false;

        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.Translate(dirX * speed * Time.deltaTime);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, leftEdge.position.x - 0.1f, rightEdge.position.x + 0.1f),
            transform.position.y);

        if (transform.position.x >= rightEdge.position.x)
            dirX = Vector2.left;
        else if (transform.position.x <= leftEdge.position.x)
            dirX = Vector2.right;
    }

    private void FixedUpdate()
    {
        if (dirX.x < 0 && !facingRight || dirX.x > 0 && facingRight)
            ChangeDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.Instance.HealthPoints--;
        StartCoroutine(DamageDelay());
        Debug.Log("Damaged");
    }

    private IEnumerator DamageDelay()
    {
        Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);

    }
}
