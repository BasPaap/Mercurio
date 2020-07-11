using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceBehaviour : MonoBehaviour
{
    public int voiceId;
    public float speed = 1.0f;
    public float range = 1.0f;  // Height of the sine wave that the voice's movement follows
    
    private Vector3 startPosition;
    private float timeOffset;   // Used to randomize the direction in which the voice travels
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(Random.Range(-1.0f, 1.0f) * range, transform.position.y, transform.position.z);
        timeOffset = Random.Range(-90.0f, 90.0f);
        audioSource = GetComponent<AudioSource>();
        ControlSystemBehaviour.VoiceToggled += ControlSystemBehaviour_VoiceToggled;
    }

    private void OnDestroy()
    {
        ControlSystemBehaviour.VoiceToggled -= ControlSystemBehaviour_VoiceToggled;
    }

    private void ControlSystemBehaviour_VoiceToggled(object sender, int e)
    {
        if (voiceId == e)
        {
            if (audioSource.isPlaying)
            {
                Debug.Log($"Stopping voice {voiceId}");
                audioSource.Stop();
            }
            else
            {
                Debug.Log($"Playing voice {voiceId}");
                audioSource.Play();                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(Mathf.Sin((timeOffset + Time.time) * speed) * range, 0, 0);
    }
}
