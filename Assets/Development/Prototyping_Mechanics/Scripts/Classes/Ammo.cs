using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool flying = false;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (flying && rigidbody.velocity != Vector3.zero)
        {
            rigidbody.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }       
    }

    internal void ActivateArrow(float ammoForce, float ammoLifetime)
    {
        rigidbody.AddForce(gameObject.transform.forward * ammoForce);
        rigidbody.useGravity = true;
        flying = true;

        Destroy(gameObject, ammoLifetime);
    }

    internal void ActivateBall(float ammoForce, float ammoLifetime)
    {
        rigidbody.AddForce(new Vector3(0, 1, 0.466f) * ammoForce);      // rot(x) = 25
        rigidbody.useGravity = true;
        flying = true;

        Destroy(gameObject, ammoLifetime);
    }
}
