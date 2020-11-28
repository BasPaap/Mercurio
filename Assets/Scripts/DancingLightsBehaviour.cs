using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingLightsBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ControlSystemBehaviour.DancingLightsToggled += ControlSystemBehaviour_DancingLightsToggled;
    }

    private void ControlSystemBehaviour_DancingLightsToggled(object sender, bool e)
    {
        gameObject.SetActive(e);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
