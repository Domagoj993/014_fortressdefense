using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionManager : MonoBehaviour
{
    public TMP_Text CoinsText;

    internal void EarnCoins(int numCoins)
    {
        CoinsText.text = "Broj zlatnika: " + CoinBag.AddCoins(numCoins);
    }

    internal void SpendCoins(int numCoins)
    {
        CoinsText.text = "Broj zlatnika: " + CoinBag.SpendCoins(numCoins);
    }
}
