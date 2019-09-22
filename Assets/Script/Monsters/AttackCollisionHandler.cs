using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionHandler : MonoBehaviour
{
    private AttackBehaviour attackBehaviour;
    private bool isColliding;
    void Start()
    {
        attackBehaviour = GetComponentInParent<AttackBehaviour>();
    }

    void Update()
    {
        isColliding = false;    
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isColliding && collider.gameObject.tag == "HitboxPlayer")
        {
            isColliding = true;
            attackBehaviour.OnHitTarget(collider.gameObject);
        }
    }
}
