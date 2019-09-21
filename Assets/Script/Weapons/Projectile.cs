using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        Vector3 vec = new Vector3(Vector2.up.x, Vector2.up.y, 0) * speed * Time.deltaTime;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("here");
        if (collider.gameObject.tag == "Monster")
        {
            DestroyProjectile();
        }
    }
}
