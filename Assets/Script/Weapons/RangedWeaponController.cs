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

    private Plane hitPlane;
    private float cameraZOffset;
    void Start()
    {
        weaponController = GetComponent<WeaponController>();
        // make hit plane along z = 0 to calculate hits from mouse click
        hitPlane = new Plane(Vector3.forward, new Vector3(0, 0, 0));

        // get camera offset for targeting calculations
        float camDistance = Camera.main.transform.position.z;
        float camAngle = 30f;
        cameraZOffset = (float)Math.Tan(camAngle * (Math.PI / 180)) * Math.Abs(camDistance); // adj * angle (toa)
    }

    void LateUpdate()
    {          
        // get mouse screen position and convert to world space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = (float)Math.Sqrt(Math.Pow(Camera.main.transform.position.z, 2f) + Math.Pow(cameraZOffset, 2f));
        Vector3 mouseInWorldSpace = Camera.main.ScreenToWorldPoint(mousePos);

        // work out angle between mouse in world space and weapon
        Vector3 difference = mouseInWorldSpace - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-30f, transform.rotation.y, rotZ - 90f); // make weapon face camera
    }

    public void FireProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (hitPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 difference = hitPoint - transform.position;
            
            // work out angle between two points
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ - 90f);

            // make projectile pointing in correct direction
            GameObject instance = Instantiate(projectile, projectileSpawn.transform.position, rotation);
            instance.GetComponent<Projectile>().damage = weaponController.getDamage();
        }
    }
}
