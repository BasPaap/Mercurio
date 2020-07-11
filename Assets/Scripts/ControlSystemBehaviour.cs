using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystemBehaviour : MonoBehaviour
{
    public static event EventHandler HandTriggered;
    public static event EventHandler<bool> DancingLightsToggled;
    public static event EventHandler<int> VoiceToggled;

    public KeyCode handTriggerKey = KeyCode.H;
    public KeyCode dancingLightsOnKey = KeyCode.L;
    public KeyCode dancingLightsOffKey = KeyCode.K;
    public KeyCode[] voiceKeys = new[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        
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

        foreach (var voiceKey in voiceKeys)
        {
            if (Input.GetKeyUp(voiceKey) && VoiceToggled != null)
            {
                int voiceId = GetNumberKeyAsInt(voiceKey);
                VoiceToggled(this, voiceId);
            }
        }
    }

    private static int GetNumberKeyAsInt(KeyCode voiceKey) => (int)voiceKey - (int)KeyCode.Alpha0;    
}
