using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLifeBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI lifeText;
    private void Start()
    {
        slider.maxValue = player.MaxLife;
        slider.value = player.CurrentLife;
        lifeText.text = $"{player.CurrentLife}/{player.MaxLife}";
    }

    void Update()
    {
        slider.value = player.CurrentLife;
        lifeText.text = $"{player.CurrentLife}/{player.MaxLife}";
    }

}