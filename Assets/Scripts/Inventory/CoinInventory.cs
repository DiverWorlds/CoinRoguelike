using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    //TODO: コインの種類で自動ソート機能を追加するかも
    public const int MinCoinCount = 1;
    public const int MaxCoinCount = 3;

    private List<Coin> coins = new List<Coin>();

    public int CoinCount => coins.Count;

    public void Add(Coin coin)
    {
        coins.Add(coin);
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

        bool isRemoved = coins.Remove(coin);
        Destroy(coin.gameObject);

        return isRemoved;
    }

    public Coin GetCoin(int index)
    {
        if (index < 0 || index >= coins.Count)
        {
            return null;
        }

        return coins[index];
    }

    public int IndexOf(Coin coin)
    {
        if (coin == null)
        {
            return -1;
        }

        return coins.IndexOf(coin);
    }
}