using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystemBehaviour : MonoBehaviour
{
    public static event EventHandler HandTriggered;
    public static event EventHandler<bool> DancingLightsToggled;

    public KeyCode handTriggerKey = KeyCode.H;
    public KeyCode dancingLightsOnKey = KeyCode.L;
    public KeyCode dancingLightsOffKey = KeyCode.K;
        
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(handTriggerKey) && HandTriggered != null)
        {
            HandTriggered(this, EventArgs.Empty);
        }

        if (Input.GetKeyUp(dancingLightsOnKey) && DancingLightsToggled != null)
        {
            DancingLightsToggled(this, true);
        }

        if (Input.GetKeyUp(dancingLightsOffKey) && DancingLightsToggled != null)
        {
            DancingLightsToggled(this, false);
        }
    }
}
