using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinBag
{
    private static int numCoins;

    internal static void InitializeCoins()
    {
        numCoins = 0;
    }

    internal static int AddCoins(int newCoins)
    {
        numCoins += newCoins;
        return numCoins;
    }

    internal static int SpendCoins(int costCoins)
    {
        numCoins -= costCoins;
        return numCoins;
    }
}
