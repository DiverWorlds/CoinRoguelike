using UnityEngine;
using TMPro;

public class CoinDescriptionWindow : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Set(string frontEffectName, string backEffectName, int frontSideValue, int backSideValue, float frontSideProbability)
    {
        descriptionText.text = GetDescriptionText(frontEffectName, backEffectName, frontSideValue, backSideValue, frontSideProbability);
        background.SetActive(true);
    }
    public void Hide()
    {
        background.SetActive(false);
    }

    private string GetDescriptionText(string frontEffectName, string backEffectName, int frontSideValue, int backSideValue, float frontSideProbability)
    {
        return $"コウカ　　{frontEffectName} / {backEffectName}\n" +
               $"マリョク　{frontSideValue} / {backSideValue} \n" +
               $"カクリツ　{frontSideProbability * 100} / {(1-frontSideProbability) * 100} %";
    }
}