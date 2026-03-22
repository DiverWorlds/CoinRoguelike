using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    [SerializeField] private CoinSlotAssorter coinSlotAssorter;
    private Action<Coin> onCoinAdded;
    private Action<Coin> onCoinRemoved;

    //TODO: コインの種類で自動ソート機能を追加するかも
    public CoinSlotAssorter CoinSlotAssorter => coinSlotAssorter;
    public const int MinCoinCount = 1;
    public const int MaxCoinCount = 3;

    private List<Coin> coins = new List<Coin>();
    
    public event Action<Coin> OnCoinAdded
    {
        add => onCoinAdded += value;
        remove => onCoinAdded -= value;
    }
    public event Action<Coin> OnCoinRemoved
    {
        add => onCoinRemoved += value;
        remove => onCoinRemoved -= value;
    }
    public int CoinCount => coins.Count;

    public void Add(Coin coin)
    {
        coins.Add(coin);
        onCoinAdded?.Invoke(coin);
    }

    public bool Remove(Coin coin)
    {
        Logger.Log("Remove called with coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
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
        onCoinRemoved?.Invoke(coin);
        Logger.Log("onCoinRemoved invoked for coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null") + ", isRemoved: " + isRemoved);

        return isRemoved;
    }
    public bool RemoveRandom()
    {
        if (coins.Count <= MinCoinCount)
        {
            return false;
        }

        int randomIndex = UnityEngine.Random.Range(0, coins.Count);
        Coin coinToRemove = coins[randomIndex];
        coins.RemoveAt(randomIndex);
        Destroy(coinToRemove.gameObject);
        onCoinRemoved?.Invoke(coinToRemove);
        Logger.Log("onCoinRemoved invoked for coin: " + (coinToRemove != null ? $"{coinToRemove.FrontSideValue} / {coinToRemove.BackSideValue}" : "null") + ", isRemoved: true");

        return true;
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