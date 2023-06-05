using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBase
{
    private Rigidbody rbody;
    private float initialForce;

    private void Start()
    {
        // Apply initial force to the bullet
        rbody = GetComponent<Rigidbody>();
        rbody.AddForce(transform.forward * initialForce, ForceMode.Impulse); // Adjust the force as per your requirements
    }

    private void Update()
    {
        // Rotate the bullet to face its velocity direction
        transform.rotation = Quaternion.LookRotation(rbody.velocity);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // Destroy the bullet
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
