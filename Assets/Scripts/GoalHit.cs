using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalHit : MonoBehaviour
{
    public UnityEvent touchGoal;
    public Transform target;
    public float heightAboveTarget = 2f;
    public bool followMarble = false;

    private void Update()
    {
        if (followMarble)
        {
            Vector3 targetPosition = target.position;
            Vector3 flagPosition = new Vector3(targetPosition.x, targetPosition.y + heightAboveTarget, targetPosition.z);
            transform.position = flagPosition;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player") && Timer.TIME_LEFT > 0)
        {
            touchGoal.Invoke();
            followMarble = true;
            Level.GameOver();
        }
    }
    void GoalSounds(string s)
    {
        AudioManager.Instance.PlaySFX(s);
    }

    
}
