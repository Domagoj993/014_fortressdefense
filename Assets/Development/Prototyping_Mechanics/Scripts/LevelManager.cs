using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        gameObject.GetComponent<ActionManager>().EarnCoins(2);
    }
}
