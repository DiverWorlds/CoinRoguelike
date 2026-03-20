using TMPro;
using UnityEngine;

public class DiscardCoinDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frontNameText;
    [SerializeField] private TextMeshProUGUI backNameText;
    [SerializeField] private TextMeshProUGUI frontStrengthText;
    [SerializeField] private TextMeshProUGUI backStrengthText;
    [SerializeField] private TextMeshProUGUI frontProbabilityText;
    [SerializeField] private TextMeshProUGUI backProbabilityText;

    private Coin coin;
    public Coin Coin
    {
        set
        {
            coin = value;
            SetCoinDetail();
        }
    }

    private void SetCoinDetail()
    {
        if (coin == null)
        {
            frontNameText.text = "";
            backNameText.text = "";
            frontStrengthText.text = "";
            backStrengthText.text = "";
            frontProbabilityText.text = "";
            return;
        }
        
        frontNameText.text = coin.FrontEffectName;
        backNameText.text = coin.BackEffectName;
        frontStrengthText.text = $"{coin.FrontSideValue}";
        backStrengthText.text = $"{coin.BackSideValue}";
        frontProbabilityText.text = $"{coin.FrontSideProbability * 100f:F1}%";
        backProbabilityText.text = $"{(1-coin.FrontSideProbability) * 100f:F1}%";
    }
}