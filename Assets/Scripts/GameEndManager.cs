using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    //データ関連
    private ScoreManager scoreManager;

    //GUI関連
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField] private Button backTitleButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        scoreText.text = "最終到達フロア: " + scoreManager.currentScore.ToString();
        highScoreText.text = "ハイスコア: " + scoreManager.highScore.ToString();
        backTitleButton.onClick.AddListener(OnClickBackTitleButton);
    }

    //GUIのボタンから呼び出される
    public void OnClickBackTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
