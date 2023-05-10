using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMarble : MonoBehaviour
{
    public float incRot = 1f;

    void FixedUpdate()
    {
        GameObject marble = this.gameObject;
        marble.transform.Rotate(-1 * incRot, incRot, 0f);
    }
}
