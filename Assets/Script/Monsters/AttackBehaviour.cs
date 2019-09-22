using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public abstract void RecieveTarget(GameObject target);
    public abstract void BasicAttack();
    public abstract void OnHitTarget(GameObject hitTarget);
    
}
