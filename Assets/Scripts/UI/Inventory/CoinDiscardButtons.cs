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
        foreach (var coinDiscardButton in coinDiscardButtons)
        {
            if (coinDiscardButton != null && coinDiscardButton.Coin != null)
            {
                coinToButtonMap[coinDiscardButton.Coin] = coinDiscardButton;
            }
        }
        InventoryManager.Instance.CoinInventory.OnCoinAdded += SetCoinDiscardButtonActivate;
        InventoryManager.Instance.CoinInventory.OnCoinRemoved += SetCoinDiscardButtonInactivate;
        Logger.Log("coinInventory action initialized");
    }
    public void SetCoinDiscardButtonActivate(Coin coin)
    {
        Logger.Log("SetCoinDiscardButtonActivate called for coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        CoinDiscardButton coinDiscardButton = coinToButtonMap[coin];
        coinDiscardButton.SetButtonActive(true);
    }
    public void SetCoinDiscardButtonInactivate(Coin coin)
    {
        Logger.Log("SetCoinDiscardButtonInactivate called for coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        CoinDiscardButton coinDiscardButton = coinToButtonMap[coin];
        coinDiscardButton.SetButtonActive(false);
    }
    public void SetCoin(int index, Coin coin)
    {
        Logger.Log("CoinDiscardButtons.SetCoin called with index: " + index + " and coin: " + (coin != null ? $"{coin.FrontSideValue} / {coin.BackSideValue}" : "null"));
        if (index >= 0 && index < coinDiscardButtons.Count)
        {
            coinDiscardButtons[index].Coin = coin;
        }
    }

}