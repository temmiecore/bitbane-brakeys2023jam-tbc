using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform target;

    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;
    public float offsetStrength = 0.1f;

    [Range(1f, 5f)]
    public float cameraDistance = 1f;

    public Vector2 handToMouseDirection;
    public Vector2 offset;

    void Start()
    {
        target = FindObjectOfType<PlayerParameters>().transform;
    }

    void Update()
    {
        handToMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position;

        Camera.main.orthographicSize = cameraDistance;
    }

    void FixedUpdate()
    {
        offset = handToMouseDirection * offsetStrength;
        Vector2 desiredPosition = new Vector2(target.position.x, target.position.y) + offset;
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }
}