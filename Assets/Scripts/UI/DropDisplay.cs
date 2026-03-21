using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDisplay : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Sprite frontIcon;
    [SerializeField] private Sprite backIcon;

    public void Initialize(string itemName, FrontAndBack frontOrBack)
    {
        itemNameText.text = itemName;
        iconImage.sprite = frontOrBack == FrontAndBack.Front ? frontIcon : backIcon;
        SetIcon(frontOrBack);
    }
    private void SetIcon(FrontAndBack frontOrBack)
    {
        if (frontOrBack == FrontAndBack.Front)
        {
            iconImage.sprite = frontIcon;
        }
        else
        {
            iconImage.sprite = backIcon;
        }
    }
}