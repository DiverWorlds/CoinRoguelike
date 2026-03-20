using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDiscardButtons : MonoBehaviour
{
    [SerializeField] private Image detailPanel;
    [SerializeField] private List<CoinDiscardButton> coinDiscardButtons;
    public Image DetailPanel => detailPanel;
    public void SetCoin(int index, Coin coin)
    {
        if (index >= 0 && index < coinDiscardButtons.Count)
        {
            coinDiscardButtons[index].Coin = coin;
        }
    }

}