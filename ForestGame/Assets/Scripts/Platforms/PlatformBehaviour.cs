using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{

    [SerializeField]
    private Collider2D platformCollider, platformTrigger;
    private Collider2D playerCollider;

    private void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
