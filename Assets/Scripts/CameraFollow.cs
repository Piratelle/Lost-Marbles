using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float rotationSpeed = 200f;
    public float liftSpeed = 5f;
    public LayerMask obstacleMask;

    private void FixedUpdate()
    {
        // Camera rotation when either shift key is held down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            float horizontalRotation = 0;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                horizontalRotation -= 1;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                horizontalRotation += 1;
            }

            // Rotate the camera offset using Quaternion multiplication
            Quaternion rotation = Quaternion.AngleAxis(horizontalRotation * rotationSpeed * Time.fixedDeltaTime, Vector3.up);
            offset = rotation * offset;
        }

        // Camera movement
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Camera lift to avoid obstacles
        RaycastHit hit;
        Vector3 rayDirection = target.position - smoothedPosition;
        float rayDistance = rayDirection.magnitude;
        rayDirection.Normalize();

        if (Physics.Raycast(smoothedPosition, rayDirection, out hit, rayDistance, obstacleMask))
        {
            smoothedPosition.y += liftSpeed * Time.fixedDeltaTime;
        }

        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
