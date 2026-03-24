using TMPro;
using UnityEngine;

public class StageDisplay : MonoBehaviour
{
    [SerializeField] private DungeonManager dungeonManager;
    [SerializeField] private TextMeshProUGUI floorText;

    void Update()
    {
        floorText.text = dungeonManager.CurrentStage.ToString("D2");
    }

}