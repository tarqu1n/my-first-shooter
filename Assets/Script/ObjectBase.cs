using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectBase : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetSortOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetSortOrder()
    {
        GetComponent<SortingGroup>().sortingOrder = (int)(transform.position.y * -100);
    }
}
