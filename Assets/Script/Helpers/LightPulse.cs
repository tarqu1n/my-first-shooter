using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightPulse : MonoBehaviour
{
    public Light2D light2D;

    public float maxIntensity;
    public float minIntensity;
    public float time;

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
            light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, currentTime / time);
        }
        else
        {
            light2D.intensity = Mathf.Lerp(maxIntensity, minIntensity, currentTime / time);
        }

        if (currentTime >= time)
        {
            currentTime = 0;
            step1 = !step1;
        }
    }
}
