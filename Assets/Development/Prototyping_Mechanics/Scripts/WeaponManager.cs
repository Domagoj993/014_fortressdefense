using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject AmmoItem;
    public Transform AmmoSpawn;
    public Transform AmmoInWeapon;
    [Space(10)]
    public float ammoForce;
    public float ammoShootingWait;
    public float ammoLifetime;
    [Space(10)]
    public bool isArrow = false;
    [Space(10)]
    public float ammoChargingDistance;
    public float ammoChargingTime;
    [Space(10)]
    public Transform Thrower;
    public float throwerForwardTime;
    public float throwerBackwardTime;

    private Transform AmmoContainer;
    private GameObject Ammo;
    private float timerShooting = 0;
    private bool ammoEquiped = false;
    private bool charging = false;
    private float z = 0;

    private void Awake()
    {
        //SpawnAmmo();
        AmmoContainer = GameObject.Find("AmmoContainer").gameObject.transform;
        timerShooting = 0;
        ammoEquiped = false;
        charging = false;
        z = 0;
}

    internal void ToggleWeapon(bool isActive)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
            if (isActive)
            {
                SpawnAmmo();
                AmmoContainer = GameObject.Find("AmmoContainer").gameObject.transform;
                timerShooting = 0;
                ammoEquiped = false;
                charging = false;
                z = 0;
            }
            else
            {
                foreach (Transform ammo in AmmoContainer)
                {
                    Destroy(ammo.gameObject);
                }
                if (AmmoSpawn.childCount != 0)
                {
                    Destroy(AmmoSpawn.GetChild(0).gameObject);
                }
                if (AmmoInWeapon.childCount != 0)
                {
                    Destroy(AmmoInWeapon.GetChild(0).gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (timerShooting == 0)
        {
            if (!ammoEquiped && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ammo.transform.SetParent(AmmoInWeapon);
                Ammo.transform.localPosition = new Vector3(0, 0, 0);

                ammoEquiped = true;
            }
            
            if (isArrow)
            {
                if (ammoEquiped && Input.GetKeyDown(KeyCode.Mouse1))
                {
                    charging = true;
                    z = 0;
                }
                else if (ammoEquiped && charging && Input.GetKey(KeyCode.Mouse1))
                {
                    z = Ammo.transform.localPosition.z - ammoChargingDistance * Time.deltaTime / (ammoChargingTime);
                    Ammo.transform.localPosition = new Vector3(Ammo.transform.localPosition.x, Ammo.transform.localPosition.y, z);
                    if (Mathf.Abs(z) > ammoChargingDistance)
                    {
                        z = -ammoChargingDistance;
                        charging = false;
                    }
                }
                else if (ammoEquiped && Input.GetKeyUp(KeyCode.Mouse1))
                {
                    Ammo.GetComponent<Ammo>().ActivateArrow(ammoForce * Mathf.Abs(z) / ammoChargingDistance, ammoLifetime);
                    Ammo.transform.SetParent(AmmoContainer);
                    timerShooting = ammoShootingWait;
                    ammoEquiped = false;
                }
            }
            else
            {
                if (ammoEquiped && Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Ammo.GetComponent<Ammo>().ActivateBall(ammoForce, ammoLifetime);
                    Ammo.transform.SetParent(AmmoContainer);
                    timerShooting = ammoShootingWait;
                    ammoEquiped = false;
                }
            }
        }
        else
        {
            float timePassed = Time.deltaTime;
            timerShooting -= timePassed;
            if (timerShooting < 0)
            {
                timerShooting = 0;
                SpawnAmmo();
            }

            if (!isArrow)
            {
                if (timerShooting >= ammoShootingWait - throwerForwardTime)
                {
                    float x = Thrower.localRotation.x + 130f / throwerForwardTime;
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
                else if (timerShooting >= 0)
                {
                    float x = Thrower.localRotation.x - 130f / throwerBackwardTime;
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
            }
        }
    }

    private void SpawnAmmo()
    {
        if (AmmoSpawn.childCount == 0)
        {
            Ammo = Instantiate(AmmoItem, AmmoSpawn.position, AmmoSpawn.rotation);
            Ammo.transform.SetParent(AmmoSpawn);
            ammoEquiped = false;
        }
    }
}
