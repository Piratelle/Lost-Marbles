using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeMarbleAnimation : MonoBehaviour
{
    private void MarbleSounds(string s)
    {
        AudioManager.Instance.PlaySFX(s);
    }
      
    private void MarbleDestroy()
    {
        Destroy(this);
    }
}
