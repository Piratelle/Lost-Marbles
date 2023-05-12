using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarbleHit : MonoBehaviour
{
    public UnityEvent touchMarble;
    public Transform target;
    public bool followMarble = false;

    public float heightAboveTarget = .7f;
    public float followForce = 20f;
    public float minDistance = .6f;
    public float maxDistance = 5f;
    public Vector3 teleportOffset = new Vector3(0, 0, 0);
    //private Rigidbody rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (followMarble)
        {
            Vector3 targetPosition = target.position;
            Vector3 marblePosition = new Vector3(targetPosition.x, heightAboveTarget, targetPosition.z);
            transform.position = marblePosition;
        }
    }
    private void FixedUpdate()
    {
        /*if (followMarble)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > maxDistance)
                {
                    // If the follower is too far away, teleport it back to a certain distance behind the target
                    transform.position = target.position + target.TransformDirection(teleportOffset);
                    rb.velocity = Vector3.zero; // Reset velocity after teleporting
                }
            else if (distance > minDistance)
                {
                    // Calculate the follow force based on the distance, making the follower move faster when it's further away
                    float adjustedForce = followForce * (distance - minDistance) / (maxDistance - minDistance);
                    adjustedForce = Mathf.Max(adjustedForce, 0);

                    Vector3 direction = (target.position - transform.position).normalized;
                    rb.AddForce(direction * adjustedForce);
                }
        }     */
    }
    private void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            touchMarble.Invoke();
//            target = GameObject.Find("PlayerMarble").transform;
            followMarble = true;
        }
    }
    private void MarbleSounds(string s)
    {
        AudioManager.Instance.PlaySFX(s);
    }
      
    private void MarbleDestroy()
    {
        Destroy(this);
    }
}
