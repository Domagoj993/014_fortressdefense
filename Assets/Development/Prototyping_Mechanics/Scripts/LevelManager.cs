using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int fortressHealth;
    [Space(10)]
    public GameObject WeaponBow;
    public GameObject WeaponCrossbow;
    public GameObject WeaponCatapult;

    private int activeWeapon;

    private void Start()
    {
        CoinBag.InitializeCoins();
        Fortress.InitializeFortress(fortressHealth);
        InitializeLevel();

        WeaponBow.GetComponent<WeaponManager>().ToggleWeapon(true);
        WeaponCrossbow.GetComponent<WeaponManager>().ToggleWeapon(false);
        WeaponCatapult.GetComponent<WeaponManager>().ToggleWeapon(false);
        activeWeapon = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponBow.GetComponent<WeaponManager>().ToggleWeapon(true);
            WeaponCrossbow.GetComponent<WeaponManager>().ToggleWeapon(false);
            WeaponCatapult.GetComponent<WeaponManager>().ToggleWeapon(false);
            activeWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponBow.GetComponent<WeaponManager>().ToggleWeapon(false);
            WeaponCrossbow.GetComponent<WeaponManager>().ToggleWeapon(true);
            WeaponCatapult.GetComponent<WeaponManager>().ToggleWeapon(false);
            activeWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponBow.GetComponent<WeaponManager>().ToggleWeapon(false);
            WeaponCrossbow.GetComponent<WeaponManager>().ToggleWeapon(false);
            WeaponCatapult.GetComponent<WeaponManager>().ToggleWeapon(true);
            activeWeapon = 2;
        }
    }

    private void InitializeLevel()
    {
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
    }
}
