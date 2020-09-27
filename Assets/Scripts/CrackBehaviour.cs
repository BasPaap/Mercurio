using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CrackBehaviour : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void OnEnable()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        this.meshRenderer.enabled = false;
        ControlSystemBehaviour.CrackTriggered += ControlSystemBehaviour_CrackTriggered;
    }

    private void ControlSystemBehaviour_CrackTriggered(object sender, System.EventArgs e)
    {
        Toggle();
    }

    private void OnDisable()
    {
        ControlSystemBehaviour.CrackTriggered -= ControlSystemBehaviour_CrackTriggered;
    }

    private void Toggle()
    {
        this.meshRenderer.enabled = !this.meshRenderer.enabled;
    }
}
