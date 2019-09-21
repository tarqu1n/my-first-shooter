using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;

    public GameObject hitAnimation;
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderTag = collider.gameObject.tag;
        if (colliderTag == "Monster")
        {
            collider.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
        }
        if (colliderTag == "Monster" || colliderTag == "Terrain")
        {

            DestroyProjectile();
            Instantiate(hitAnimation, transform.position, Quaternion.identity);
        }
    }
}
