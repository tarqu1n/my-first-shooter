using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectileSpawn;
    void Start()
    {
        
    }

    void LateUpdate()
    {

        // Camera offset
        float camDistance = Camera.main.transform.position.z;
        float camAngle = 30f;
        float offset = (float)Math.Tan(camAngle * (Math.PI / 180)) * Math.Abs(camDistance); // adj * angle (toa)

        // make weapon face camera
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = (float)Math.Sqrt(Math.Pow(Camera.main.transform.position.z, 2f) + Math.Pow(offset, 2f));
        Vector3 mouseInWorldSpace = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 difference = mouseInWorldSpace - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ - 90f);
    }

    public void FireProjectile()
    {
        Instantiate(projectile, projectileSpawn.transform.position, transform.rotation);
    }
}
