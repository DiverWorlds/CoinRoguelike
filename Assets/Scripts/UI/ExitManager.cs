using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private DungeonManager dungeonManager;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnExitButtonClicked);
    }

    public void ShowExit()
    {
        button.gameObject.SetActive(true);
    }
    public void HideExit()
    {
        button.gameObject.SetActive(false);
    }
    private void OnExitButtonClicked()
    {
        ScoreManager.Instance.UpdateScores(dungeonManager.CurrentStage);
        SceneManager.LoadScene("GameClear");
    }

}