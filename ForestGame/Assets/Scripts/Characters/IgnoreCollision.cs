using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private Collider2D target, player;

    private void Awake()
    {
        Physics2D.IgnoreCollision(player, target, true);
    }
}
