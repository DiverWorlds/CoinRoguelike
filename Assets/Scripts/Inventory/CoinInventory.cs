using System.Collections.Generic;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    //TODO: コインの種類で自動ソート機能を追加するかも
    public const int MinCoinCount = 1;
    public const int MaxCoinCount = 3;

    private List<Coin> coins = new List<Coin>();

    public int CoinCount => coins.Count;

    public bool Add(Coin coin)
    {
        if (coin == null)
        {
            return false;
        }

        if (coins.Count >= MaxCoinCount)
        {
            return false;
        }

        coins.Add(coin);
        return true;
    }

    public bool Remove(Coin coin)
    {
        if (coin == null)
        {
            return false;
        }

        if (coins.Count <= MinCoinCount)
        {
            return false;
        }

        return coins.Remove(coin);
    }

    public Coin GetCoin(int index)
    {
        if (index < 0 || index >= coins.Count)
        {
            return null;
        }

        return coins[index];
    }
}