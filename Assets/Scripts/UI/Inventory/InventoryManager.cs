using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private SideInventory sideInventory;
    [SerializeField] private CoinInventory coinInventory;
    [SerializeField] private CoinDiscardButtons coinDiscardButtons;

    public SideInventory SideInventory => sideInventory;
    public CoinInventory CoinInventory => coinInventory;
    public CoinDiscardButtons CoinDiscardButtons => coinDiscardButtons;

    public void OnInventoryOpened()
    {
        Logger.Log("OnInventoryOpened called");
        SetInitialCoins();
    }

    private void SetInitialCoins()
    {
        Logger.Log("SetInitialCoins called");
        for (int i = 0; i < coinInventory.CoinCount; i++)
        {
            coinDiscardButtons.SetCoin(i, coinInventory.GetCoin(i));
        }
    }
}