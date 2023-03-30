using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    //Inspired by Jakob Knop Rasmussen
    [SerializeField]
    private Rigidbody projectilePrefab;

    [SerializeField]
    private float firingForce = 3000f;

    [SerializeField]
    private Transform firingPoint;
    
    void Awake()
    {
        GetComponent<AutoFiringScript>()?.OnFire += Shoot;
    }

    private void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        projectile.AddForce(projectile.transform.forward * firingForce);
    }
}
