using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightFlash : MonoBehaviour
{
    public Light2D light2D;

    public float buildTime;
    public float dissapateTime;
    public float peakIntensity;

    private float currentTime;
    private bool step1 = true;
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (step1)
        {
            light2D.intensity = Mathf.Lerp(0f, peakIntensity, currentTime / buildTime);
        }
        else
        {
            light2D.intensity = Mathf.Lerp(peakIntensity, 0f, currentTime / dissapateTime);
        }

        if (step1 && currentTime >= buildTime)
        {
            currentTime = 0;
            step1 = false;
        } 
        else if (currentTime > dissapateTime)
        {
            Destroy(gameObject);
        }
    }
}
