using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [Range(0, 1.0f)]
    public float progress;
    public AnimationCurve fadeInCurve;

    private new Renderer renderer;
    private int progressPropertyId;
    
    
    void Start()
    {
        progressPropertyId = Shader.PropertyToID("_Progress");
        renderer = GetComponent<Renderer>();        
    }

    void Update()
    {        
        renderer.material.SetFloat(progressPropertyId, fadeInCurve.Evaluate(progress));
    }
}
