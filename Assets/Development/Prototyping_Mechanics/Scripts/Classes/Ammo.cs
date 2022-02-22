using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody rbody;
    private bool flying = false;

    private void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (flying && rbody.velocity != Vector3.zero)
        {
            rbody.rotation = Quaternion.LookRotation(rbody.velocity);
        }       
    }

    internal void ActivateArrow(float ammoForce, float ammoLifetime)
    {
        rbody.AddForce(gameObject.transform.forward * ammoForce);
        rbody.useGravity = true;
        flying = true;

        Destroy(gameObject, ammoLifetime);
    }

    internal void ActivateBall(float ammoForce, float ammoLifetime)
    {
        rbody.AddRelativeForce(new Vector3(0, 1, 0.7f) * ammoForce);      // rot(x) = 35
        rbody.useGravity = true;
        flying = true;

        Destroy(gameObject, ammoLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Troop"))
        {
            GameObject.Find("GameManager").GetComponent<ActionManager>().EarnCoins(2);

            // Enemy killed
            Destroy(other.gameObject);

            // Remove ammo
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            // Remove ammo
            Destroy(gameObject);
        }
    }
}
