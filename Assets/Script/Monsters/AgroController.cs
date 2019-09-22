using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroController : MonoBehaviour
{
    public float radius;

    private MonsterController monsterController;
    private CircleCollider2D collider;
    void Start()
    {
        monsterController = GetComponentInParent<MonsterController>();

        collider = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
        collider.radius = radius;
        collider.isTrigger = true;
    }

    void CreateCollider ()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            monsterController.currentBehaviour = MonsterController.Behaviour.Attack;
            collider.enabled = false;
        }
    }

    public void Enable()
    {
        collider.enabled = true;
    }
}
