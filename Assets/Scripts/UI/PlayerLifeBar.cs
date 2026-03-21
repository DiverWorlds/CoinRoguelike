using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLifeBar : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI lifeText;
    private void Start()
    {
        slider.maxValue = character.MaxLife;
        slider.value = character.CurrentLife;
        lifeText.text = $"{character.CurrentLife}/{character.MaxLife}";
    }

    void Update()
    {
        slider.value = character.CurrentLife;
        lifeText.text = $"{character.CurrentLife}/{character.MaxLife}";
    }

}