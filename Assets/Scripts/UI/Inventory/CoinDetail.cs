using TMPro;
using UnityEngine;

public class CoinDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frontNameText;
    [SerializeField] private TextMeshProUGUI backNameText;
    [SerializeField] private TextMeshProUGUI frontStrengthText;
    [SerializeField] private TextMeshProUGUI backStrengthText;
    [SerializeField] private TextMeshProUGUI frontProbabilityText;
    [SerializeField] private TextMeshProUGUI backProbabilityText;
    [SerializeField] private TextMeshProUGUI bonusText;

    private Coin coin;
    public Coin Coin
    {
        get => coin;
        set
        {
            coin = value;
            SetCoinDetail();
        }
    }

    void Start()
    {
        SetCoinDetail();
    }

    private void SetCoinDetail()
    {
        Logger.Log("SetCoinDetail called)");
        if (coin == null)
        {
            Logger.Log("Coin is null");
            frontNameText.text = "";
            backNameText.text = "";
            frontStrengthText.text = "";
            backStrengthText.text = "";
            frontProbabilityText.text = "";
            backProbabilityText.text = "";
            bonusText.text = "";
            return;
        }
        
        frontNameText.text = coin.FrontEffectName;
        backNameText.text = coin.BackEffectName;
        frontStrengthText.text = $"{coin.FrontSideValue}";
        backStrengthText.text = $"{coin.BackSideValue}";
        frontProbabilityText.text = $"{coin.FrontSideProbability * 100f:F1}%";
        backProbabilityText.text = $"{(1-coin.FrontSideProbability) * 100f:F1}%";
        if (bonusText != null) bonusText.text = $"ウラ面ボーナス x {coin.BackSideBonus:F2}";
    }
}