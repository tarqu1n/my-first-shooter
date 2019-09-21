using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TerrainController : MonoBehaviour
{
    private SortingGroup sortingGroup;
    void Start()
    {
        sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingOrder = (int)(transform.position.y * -100);
    }

    void Update()
    {
        
    }
}
