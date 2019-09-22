using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject equipped;
    public int startHealth;

    [Header("Current Values")]
    public int currentHealth;
    public int currentMaxHealth;

    void Start()
    {
        currentHealth = currentMaxHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveDamage(int damage)
    {
        currentHealth -= damage;
    }
}
