using UnityEngine;
using TMPro;

public class CoinDescriptionWindow : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void Set(string frontEffectName, string backEffectName, int frontSideValue, int backSideValue, float frontSideProbability)
    {
        Debug.Log($"CoinDescriptionWindow.Set() called: front={frontEffectName}, back={backEffectName}, frontVal={frontSideValue}, backVal={backSideValue}, prob={frontSideProbability}");
        Debug.Log($"descriptionText: {(descriptionText != null ? "NOT NULL" : "NULL")}, background: {(background != null ? "NOT NULL" : "NULL")}");
        descriptionText.text = GetDescriptionText(frontEffectName, backEffectName, frontSideValue, backSideValue, frontSideProbability);
        background.SetActive(true);
        Debug.Log("CoinDescriptionWindow.Set() completed");
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