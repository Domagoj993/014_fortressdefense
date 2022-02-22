using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    public GameObject AmmoItem;
    public Transform AmmoSpawn;
    public Transform AmmoInWeapon;
    [Space(10)]
    public float ammoForce;
    public float ammoShootingWait;
    public float ammoLifetime;
    [Space(10)]
    public Transform Thrower;
    public float throwerForwardTime;
    [Space(10)]
    public bool AI = false;

    private Transform AmmoContainer;
    private GameObject Ammo;
    private float timerShooting = 0;
    private bool ammoEquiped = false;

    private void Awake()
    {
        AmmoContainer = GameObject.Find("AmmoContainer").gameObject.transform;
        timerShooting = 0;
        ammoEquiped = false;

        if (AI)
        {
            Ammo = Instantiate(AmmoItem, AmmoSpawn);
            Ammo.transform.SetParent(AmmoSpawn);
            ammoEquiped = false;
        }
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
        if (AI)
        {
            if (timerShooting == 0)
            {
                if (!ammoEquiped)
                {
                    Ammo.transform.SetParent(AmmoInWeapon);
                    Ammo.transform.localPosition = new Vector3(0, 0, 0);

                    ammoEquiped = true;
                }
                else if (ammoEquiped)
                {
                    Ammo.GetComponent<Ammo>().ActivateBall(ammoForce, ammoLifetime);
                    //Ammo.transform.SetParent(AmmoContainer);
                    timerShooting = ammoShootingWait;
                    ammoEquiped = false;
                }
            }
            else
            {
                float timePassed = Time.deltaTime;
                timerShooting -= timePassed;
                if (timerShooting < 0)
                {
                    timerShooting = 0;

                    Ammo = Instantiate(AmmoItem, AmmoSpawn);
                    Ammo.transform.SetParent(AmmoSpawn);
                    ammoEquiped = false;
                }

                if (timerShooting >= ammoShootingWait - throwerForwardTime)
                {
                    float x = Thrower.localRotation.x + 110f / throwerForwardTime;
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
                else if (timerShooting > 0)
                {
                    float x = Thrower.localRotation.x - 110f / (ammoShootingWait - throwerForwardTime);
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
                else if (timerShooting == 0)
                {
                    Thrower.localRotation = Quaternion.Euler(35, 0, 0);
                }
            }
        }


        // --- Legacy ---
        else
        {
            if (timerShooting == 0)
            {
                if (!ammoEquiped && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ammo.transform.SetParent(AmmoInWeapon);
                    Ammo.transform.localPosition = new Vector3(0, 0, 0);

                    ammoEquiped = true;
                }

                if (ammoEquiped && Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Ammo.GetComponent<Ammo>().ActivateBall(ammoForce, ammoLifetime);
                    Ammo.transform.SetParent(AmmoContainer);
                    timerShooting = ammoShootingWait;
                    ammoEquiped = false;
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

                if (timerShooting >= ammoShootingWait - throwerForwardTime)
                {
                    float x = Thrower.localRotation.x + 110f / throwerForwardTime;
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
                else if (timerShooting > 0)
                {
                    float x = Thrower.localRotation.x - 110f / (ammoShootingWait - throwerForwardTime);
                    Thrower.Rotate(new Vector3(x, 0, 0) * timePassed);
                }
                else if (timerShooting == 0)
                {
                    Thrower.localRotation = Quaternion.Euler(35, 0, 0);
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
