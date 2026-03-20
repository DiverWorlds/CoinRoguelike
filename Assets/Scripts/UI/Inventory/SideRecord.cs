using UnityEngine;
using TMPro;

public class SideRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI weightText;
    private BaseSide side;

    public BaseSide Side => side;

    public void Initialize(BaseSide side)
    {
        this.side = side;
        nameText.text = side.EffectName;
        strengthText.text = side.Strength.ToString();
        weightText.text = side.Weight.ToString();
    }
}