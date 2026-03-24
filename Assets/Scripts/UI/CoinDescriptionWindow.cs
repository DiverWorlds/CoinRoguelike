using UnityEngine;
using TMPro;

public class CoinDescriptionWindow : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private CoinDetail coinDetail;

    public void Set(Coin coin)
    {
        background.SetActive(true);
        coinDetail.Coin = coin;
    }
    public void Hide()
    {
        background.SetActive(false);
    }

}