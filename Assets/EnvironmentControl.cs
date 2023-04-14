using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControl : MonoBehaviour
{
    public float tiltSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xTilt = 0;
        if (Input.GetKey(KeyCode.Q)){
            xTilt += -1;
        }
        if (Input.GetKey(KeyCode.E)){
            xTilt += 1;
        }

        transform.Rotate(xTilt * tiltSpeed * Time.deltaTime, 0, 0, Space.Self);
    }
}
