using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-30f, 0f, 0f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
