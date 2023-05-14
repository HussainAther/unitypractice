using UnityEngine;

public class CameraController : MonoBehaviour // Code for camera movement alongside player.
{
    public Transform target;
    public float cameraDistance = 10.0f;
    public float cameraHeight = 2.0f;
    public float cameraSpeed = 3.0f;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0.0f, cameraHeight, -cameraDistance);
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

