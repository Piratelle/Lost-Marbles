using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float rotationSpeed = 100f;
    public float maxLiftHeight = 5f;
    public LayerMask obstacleMask;

//  public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public KeyCode switchCameraKey = KeyCode.C;

    private void Start()
    {
        thirdPersonCamera.enabled = true;
//      firstPersonCamera.enabled = false;
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(switchCameraKey))
        {
            thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
        }*/
    }
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
        Vector3 rayDirection = new Vector3(smoothedPosition.x - target.position.x, 0, smoothedPosition.z - target.position.z);
        float rayDistance = rayDirection.magnitude;
        rayDirection.Normalize();

        if (Physics.Raycast(target.position, rayDirection, out hit, rayDistance, obstacleMask))
        {
            float targetDistance = Vector3.Distance(hit.point, target.position);
            float liftHeight = Mathf.Lerp(0, maxLiftHeight, 1 - (targetDistance / rayDistance));
            smoothedPosition.y += liftHeight;
            Debug.DrawLine(target.position, hit.point, Color.white, 10f);
        }

        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}