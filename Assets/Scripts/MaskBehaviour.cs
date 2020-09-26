using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        ControlSystemBehaviour.MaskTriggered += ControlSystemBehaviour_MaskTriggered;
    }

    private void ControlSystemBehaviour_MaskTriggered(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        ControlSystemBehaviour.MaskTriggered -= ControlSystemBehaviour_MaskTriggered;
    }
}
