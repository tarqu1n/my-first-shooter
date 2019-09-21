using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class RangedWeaponController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectileSpawn;

    private WeaponController weaponController;
    void Start()
    {
        weaponController = GetComponent<WeaponController>();
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
        transform.rotation = Quaternion.Euler(-30f, transform.rotation.y, rotZ - 90f);
    }

    public void FireProjectile()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);
        GameObject instance = Instantiate(projectile, projectileSpawn.transform.position, rotation);
        instance.GetComponent<Projectile>().damage = weaponController.getDamage();
    }
}
