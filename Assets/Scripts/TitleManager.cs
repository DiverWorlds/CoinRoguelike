using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //データ関連
    private ScoreManager scoreManager;
    //GUI関連
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Canvas CreditCanvas;
    [SerializeField] private Button closeCreditButton;
    private void Start()
    {
        scoreManager = ScoreManager.Instance;
        CreditCanvas.enabled = false;
    }
    public void OnClickStartButton()
    {
        scoreManager.ResetScore();
        SceneManager.LoadScene("BattleScene");
    }
    public void OnClickCreditButton()
    {
        CreditCanvas.enabled = true;
    }
    public void OnClickCloseCreditButton()
    {
        CreditCanvas.enabled = false;
    }
}
