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
    [Space(10)]
    public Transform EnemiesContainer;
    public GameObject[] Enemies;
    public float[] enemySpawnX;
    public int[] enemyTimers;

    [SerializeField]
    private float timePassed = 0;
    private int nextEnemy = 0;

    private void Start()
    {
        CoinBag.InitializeCoins();
        Fortress.InitializeFortress(fortressHealth);
        //InitializeLevel();

        WeaponBow.GetComponent<BowManager>().ToggleWeapon(true);
        WeaponCrossbow.GetComponent<CrossbowManager>().ToggleWeapon(false);
        //WeaponCatapult.GetComponent<CatapultManager>().ToggleWeapon(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponBow.GetComponent<BowManager>().ToggleWeapon(true);
            WeaponCrossbow.GetComponent<CrossbowManager>().ToggleWeapon(false);
            //WeaponCatapult.GetComponent<CatapultManager>().ToggleWeapon(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponBow.GetComponent<BowManager>().ToggleWeapon(false);
            WeaponCrossbow.GetComponent<CrossbowManager>().ToggleWeapon(true);
            //WeaponCatapult.GetComponent<CatapultManager>().ToggleWeapon(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponBow.GetComponent<BowManager>().ToggleWeapon(false);
            WeaponCrossbow.GetComponent<CrossbowManager>().ToggleWeapon(false);
            //WeaponCatapult.GetComponent<CatapultManager>().ToggleWeapon(true);
        }

        timePassed += Time.deltaTime;
        if (nextEnemy < enemyTimers.Length && timePassed >= enemyTimers[nextEnemy])
        {
            GameObject enemy = Instantiate(Enemies[nextEnemy], EnemiesContainer);
            enemy.transform.localPosition = new Vector3(enemySpawnX[nextEnemy], 0.9f, 58);

            if (enemy.GetComponent<Troop>() != null)
            {
                enemy.GetComponent<Troop>().InitializeTroop(58, 60);
            }
            nextEnemy++;
        }
    }

    private void InitializeLevel()
    {
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
    }
}
