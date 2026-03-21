using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SideRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private Image backgroundImage;
    private SideRecordsManager sideRecordsmanager;
    private BaseSide side;

    public BaseSide Side => side;

    public void Initialize(BaseSide side, SideRecordsManager manager)
    {
        this.side = side;
        nameText.text = side.EffectName;
        strengthText.text = side.Strength.ToString();
        weightText.text = side.Weight.ToString();
        this.sideRecordsmanager = manager;
    }

    public void OnClick()
    {
        Logger.Log($"Clicked on {side.EffectName}, Strength: {side.Strength}, Weight: {side.Weight}");
        sideRecordsmanager.SelectRecord(this);
    }
    public void ToggleImageVisualize(bool enabled)
    {
        Color color = backgroundImage.color;
        color.a = enabled ? 1f : 0f;
        backgroundImage.color = color;
    }
}