using UnityEngine;
public class ScoreManager : DontDestroySingleton<ScoreManager>
{
    public int currentScore = 0;
    public int highScore = 0;
    private bool isNotified = false;
    public void UpdateScores(int currentScore)
    {
        this.currentScore = currentScore;
        if (this.currentScore > highScore)
        {
            highScore = this.currentScore;

            //ハイスコア更新の処理？バトル中であれば一度だけ呼べれば十分か
            if (isNotified)
            {
                NotifyHighScoreChanged();
                isNotified = true;
            }
        }
    }
    public void ResetScore()
    {
        currentScore = 0;
        isNotified = false;
    }
    public void NotifyHighScoreChanged()
    {
        //ハイスコアが更新されたときの処理
        Logger.Log("New High Score: " + highScore);
    }
}
