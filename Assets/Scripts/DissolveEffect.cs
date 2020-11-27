using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public float spawnEffectTime = 2;
    public float pauseTime = 1;
    public AnimationCurve fadeInCurve;

    private float elapsedTime = 0;
    private new Renderer renderer;
    private int progressPropertyId;

    void Start()
    {
        progressPropertyId = Shader.PropertyToID("_Progress");
        renderer = GetComponent<Renderer>();        
    }

    void Update()
    {
        if (elapsedTime < spawnEffectTime + pauseTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {            
            elapsedTime = 0;
        }

        renderer.material.SetFloat(progressPropertyId, fadeInCurve.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, elapsedTime)));
    }
}
