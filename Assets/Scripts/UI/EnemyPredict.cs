using TMPro;
using UnityEngine;

public class EnemyPredict : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private TextMeshProUGUI skillNameText;

    private void FixedUpdate()
    {
        skillNameText.text = enemy.NextSkill.skillName;
    }

}