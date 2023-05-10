using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 firstPersonOffset;
    public float rotationSpeed = 100f;
    
    public float smoothSpeed = .225f;

    private float currentYRotation;

    private void Start()
    {
        currentYRotation = target.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        transform.position = target.position + firstPersonOffset;

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

            currentYRotation += horizontalRotation * rotationSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentYRotation = Mathf.Lerp(currentYRotation, target.eulerAngles.y, Time.fixedDeltaTime * smoothSpeed);
        }

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }
}