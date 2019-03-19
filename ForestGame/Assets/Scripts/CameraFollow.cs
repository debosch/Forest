using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
 
    private Transform player;

    private readonly float minX = 0f, maxY = 3.24f, minY = -0.28f;

    private void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        camera.transform.position = new Vector3(
            Mathf.Clamp(player.position.x, minX, player.position.x + 13),
            Mathf.Clamp(player.position.y, minY, maxY),
            camera.transform.position.z);
    }
}
