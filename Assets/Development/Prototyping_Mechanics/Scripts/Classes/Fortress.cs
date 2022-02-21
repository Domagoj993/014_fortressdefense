using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fortress
{
    private static int health;

    internal static void InitializeFortress(int totalHealth)
    {
        health = totalHealth;
    }
}
