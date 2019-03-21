using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private Collider2D target;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target, true);
    }
}
