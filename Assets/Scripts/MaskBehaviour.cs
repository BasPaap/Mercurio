using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MaskBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject maskModel;
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();     
        ControlSystemBehaviour.MaskTriggered += ControlSystemBehaviour_MaskTriggered;
    }

    private void ControlSystemBehaviour_MaskTriggered(object sender, System.EventArgs e)
    {
        Play();
    }

    private void OnDisable()
    {
        ControlSystemBehaviour.MaskTriggered -= ControlSystemBehaviour_MaskTriggered;
    }

    private void Play()
    {        
        animator.SetTrigger("Reveal");
    }
}