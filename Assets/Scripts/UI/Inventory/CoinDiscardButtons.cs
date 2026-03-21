using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDiscardButtons : MonoBehaviour
{
    [SerializeField] private Image detailPanel;
    [SerializeField] private CoinDetail coinDetail;
    [SerializeField] private List<CoinDiscardButton> coinDiscardButtons;
    private Dictionary<Coin, CoinDiscardButton> coinToButtonMap = new Dictionary<Coin, CoinDiscardButton>();
    public Image DetailPanel => detailPanel;
    public CoinDetail CoinDetail => coinDetail;
    void Start()
    {
        InventoryManager.Instance.CoinInventory.OnCoinAdded += SetCoinDiscardButtonActivate;
        InventoryManager.Instance.CoinInventory.OnCoinRemoved += SetCoinDiscardButtonInactivate;
        Logger.Log("coinInventory action initialized");
    }
    public void SetCoinDiscardButtonActivate(Coin coin)
    {
        Logger.Log("SetCoinDiscardButtonActivate called for coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        if (coin == null)
        {
            return;
        }

        if (!coinToButtonMap.TryGetValue(coin, out CoinDiscardButton coinDiscardButton))
        {
            int index = InventoryManager.Instance.CoinInventory.IndexOf(coin);
            if (index >= 0)
            {
                SetCoin(index, coin);
                coinToButtonMap.TryGetValue(coin, out coinDiscardButton);
            }
        }

        if (coinDiscardButton == null)
        {
            Logger.Log($"No discard button mapping found for coin: {coin.FrontSideValue} / {coin.BackSideValue}");
            return;
        }

        coinDiscardButton.SetButtonActive(true);
    }
    public void SetCoinDiscardButtonInactivate(Coin coin)
    {
        Logger.Log("SetCoinDiscardButtonInactivate called for coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        if (coin == null)
        {
            return;
        }

        if (!coinToButtonMap.TryGetValue(coin, out CoinDiscardButton coinDiscardButton) || coinDiscardButton == null)
        {
            Logger.Log($"No discard button mapping found while inactivating: {coin.FrontSideValue} / {coin.BackSideValue}");
            return;
        }

        coinToButtonMap.Remove(coin);
        coinDiscardButton.Coin = null;
        coinDiscardButton.SetButtonActive(false);
    }
    public void SetCoin(int index, Coin coin)
    {
        Logger.Log("CoinDiscardButtons.SetCoin called with index: " + index + " and coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        if (index >= 0 && index < coinDiscardButtons.Count)
        {
            CoinDiscardButton button = coinDiscardButtons[index];
            Coin oldCoin = button.Coin;
            if (oldCoin != null)
            {
                coinToButtonMap.Remove(oldCoin);
            }

            button.Coin = coin;
            if (coin != null)
            {
                coinToButtonMap[coin] = button;
            }
        }
    }

}