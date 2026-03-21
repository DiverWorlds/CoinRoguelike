using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SideRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite frontIconSprite;
    [SerializeField] private Sprite backIconSprite;
    private FrontAndBack frontOrBack;
    void Start()
    {
        iconImage.sprite = frontOrBack == FrontAndBack.Front ? frontIconSprite : backIconSprite;
    }
    private SideRecordsManager sideRecordsmanager;
    private BaseSide side;
    private SidesPanel sidesPanel;

    public BaseSide Side => side;

    public void Initialize(BaseSide side, SideRecordsManager manager, SidesPanel sidesPanel, FrontAndBack frontOrBack)
    {
        this.side = side;
        this.frontOrBack = frontOrBack;
        nameText.text = side.EffectName;
        strengthText.text = side.Strength.ToString();
        weightText.text = side.Weight.ToString();
        this.sideRecordsmanager = manager;
        this.sidesPanel = sidesPanel;
    }

    public void OnClick()
    {
        Logger.Log($"Clicked on {side.EffectName}, Strength: {side.Strength}, Weight: {side.Weight}");
        sideRecordsmanager.SelectRecord(this);
        sidesPanel.AddSelectedRecord(this);
    }
    public void ToggleImageVisualize(bool enabled)
    {
        Color color = backgroundImage.color;
        color.a = enabled ? 1f : 0f;
        backgroundImage.color = color;
    }
}