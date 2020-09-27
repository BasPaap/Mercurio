using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class HandBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Number of seconds to wait until activating the kicker.")]
    private float kickDelay;

    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Prepare();
    }

    private void Play()
    {
        videoPlayer.Play();
        this.Wait(kickDelay, () => HardwareHostBehaviour.SendCommand(HardwareCommand.Kick));        
    }

    private void OnEnable()
    {
        ControlSystemBehaviour.HandTriggered += ControlSystemBehaviour_HandTriggered;        
    }

    private void OnDisable()
    {
        ControlSystemBehaviour.HandTriggered -= ControlSystemBehaviour_HandTriggered;        
    }

    private void ControlSystemBehaviour_HandTriggered(object sender, System.EventArgs e)
    {
        Play();
    }
}
