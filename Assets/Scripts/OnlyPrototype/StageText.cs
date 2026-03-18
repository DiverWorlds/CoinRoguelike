using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageText : MonoBehaviour
{
    [SerializeField] private string prefix = "Stage ";

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (BattleManager.Instance == null)
        {
            return;
        }

        textComponent.text = prefix + BattleManager.Instance.CurrentStage;
    }
}
