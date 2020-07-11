using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class HandBehaviour : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Prepare();
    }

    public void Play()
    {
        videoPlayer.Play();
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
