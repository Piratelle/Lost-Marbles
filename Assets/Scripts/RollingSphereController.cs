using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSphereController : MonoBehaviour
{
    public float pitchScale = 20.0f;
    public float volumeScale = 15.0f;
    public float moveSpeed = 20.0f;
    public bool grounded = true;
    private Rigidbody rb;
    public float minSpeedForSound = 0.1f;
    public float groundCheckDistance = 0.75f;
    public LayerMask groundLayer;
    private AudioSource[] audioS;
    private Camera mainCamera;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponents<AudioSource>();
        mainCamera = Camera.main;
    }


    private void Update()
    {       
        RollingSound();
    }
    private void FixedUpdate()
    {
        Movement();
        grounded = IsGrounded();
    }

    private void Movement()
    {
        float moveHorizontal = 0;
        float moveVertical = 0; 

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveVertical += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveVertical -= 1;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                moveHorizontal -= 1;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                moveHorizontal += 1;
            }
        }
        

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 relativeMovement = cameraForward * movement.z + cameraRight * movement.x;
        if (IsGrounded()){
        rb.AddForce(relativeMovement * moveSpeed);
        }
        else
        {
        rb.AddForce(relativeMovement * moveSpeed * .2f);
        }
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
    private void RollingSound()
    {
        if (IsGrounded() && rb.velocity.magnitude >= minSpeedForSound && !audioS[0].isPlaying)
        {
            audioS[0].Play();
        }
        else if (!IsGrounded() || rb.velocity.magnitude < minSpeedForSound)
        {
            audioS[0].Pause();
        }
        // Update pitch based on the speed of the sphere
        audioS[0].pitch = Mathf.Clamp(rb.velocity.magnitude / pitchScale, 0.1f, 2.0f);
        audioS[0].volume = Mathf.Clamp(rb.velocity.magnitude / volumeScale, 0.0f, 2.0f);

    }

}