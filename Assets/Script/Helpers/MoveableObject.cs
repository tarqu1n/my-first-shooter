using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MoveableObject : MonoBehaviour
{
    private SortingGroup sortingGroup;
    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingOrder = (int)(transform.position.y * -100);
    }

    void FixedUpdate()
    {
        // set the layer based on y;
        sortingGroup.sortingOrder = (int)(transform.position.y * -100);
    }
}
