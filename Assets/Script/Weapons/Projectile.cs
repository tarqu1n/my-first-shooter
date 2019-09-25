using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public ProjectileOwnerType ownerType = ProjectileOwnerType.Player;

    public GameObject hitAnimation;
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderTag = collider.gameObject.tag;

        if (ownerType == ProjectileOwnerType.Monster)
        {
            if (colliderTag == "HitboxPlayer")
            {
                collider.gameObject.GetComponentInParent<PlayerController>().RecieveDamage(damage);
                
            } else if (colliderTag == "HitboxShard")
            {
                collider.gameObject.GetComponentInParent<ShardController>().TakeDamage(damage);
            }

            if (colliderTag == "HitboxPlayer" || colliderTag == "HitboxShard")
            {
                DestroyProjectile();

                if (hitAnimation)
                {
                    Instantiate(hitAnimation, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (colliderTag == "HitboxMonster")
            {
                collider.gameObject.GetComponentInParent<MonsterController>().TakeDamage(damage);
            }
            if (colliderTag == "HitboxMonster" || colliderTag == "HitboxTerrain")
            {
                DestroyProjectile();

                if (hitAnimation)
                {
                    Instantiate(hitAnimation, transform.position, Quaternion.identity);
                }
            }
        }
        
    }

    public enum ProjectileOwnerType
    {
        Monster,
        Player
    }
}
