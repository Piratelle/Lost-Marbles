using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentControl : MonoBehaviour
{
    public float changeTiltChance = 0.05f;
    public float changeTiltDirChance = 0.1f;
    public float tiltSpeed = 5f;

    bool randomTilt = false;
    bool tiltPos = true;
    float maxTilt = 45f;

    GameObject enviro;

    // Start is called before the first frame update
    void Start()
    {
        enviro = this.gameObject;

        // check relevant player prefs
        if (PlayerPrefs.HasKey("TiltRand")) randomTilt = (PlayerPrefs.GetInt("TiltRand") > 0);
    }

    // Update is called once per frame
    void Update()
    {
        // handle tilting
        bool tilt = false;
        if (!randomTilt)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                tiltPos = false;
                tilt = true;
            }
            if (Input.GetKey(KeyCode.E))
            {
                tiltPos = true;
                tilt = true;
            }
        }
        else
        {
            if (Random.value < changeTiltDirChance) tiltPos = !tiltPos;
            tilt = (Random.value < changeTiltChance);
        }
        if ((tiltPos && enviro.transform.rotation.eulerAngles.x >= maxTilt && enviro.transform.rotation.eulerAngles.x < 180) 
            || (!tiltPos && enviro.transform.rotation.eulerAngles.x <= 360 - maxTilt && enviro.transform.rotation.eulerAngles.x > 180)) tilt = false;
        if (tilt) enviro.transform.Rotate((tiltPos ? 1 : -1) * tiltSpeed * Time.deltaTime, 0f, 0f);
    }
}
