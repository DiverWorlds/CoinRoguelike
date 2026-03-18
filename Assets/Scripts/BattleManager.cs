//TODO: Player, Enemyの順に行動を促す役割
//TODO: DungeonManagerからステージ数に応じた敵の情報を受け取る役割
//TODO: 勝利, 敗北判定を行う役割

using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DungeonManager dungeonManager;
    private Enemy enemy;

    private int currentTurn = 1;
    private bool waitingForPlayerCoinEffect;
    private bool isBattleEnded;

    public int CurrentTurn => currentTurn;
    public bool IsWaitingForPlayerCoinEffect => waitingForPlayerCoinEffect;

    public void SetEnemy(Enemy stageEnemy)
    {
        enemy = stageEnemy;
    }

    public void StartBattle()
    {
        currentTurn = 1;
        waitingForPlayerCoinEffect = false;
        isBattleEnded = false;
        ProcessTurn();
    }

    public bool ExecutePlayerCoinEffect(int coinIndex)
    {
        if (isBattleEnded || !waitingForPlayerCoinEffect || player == null || enemy == null)
        {
            return false;
        }

        CoinInventory coinInventory = player.CoinInventory;
        if (coinInventory == null)
        {
            return false;
        }

        Coin coin = coinInventory.GetCoin(coinIndex);
        if (coin == null)
        {
            return false;
        }

        coin.Effect(enemy);

        if (CheckBattleOutcome())
        {
            return true;
        }

        waitingForPlayerCoinEffect = false;
        currentTurn++;
        ProcessTurn();
        return true;
    }

    private void ProcessTurn()
    {
        if (isBattleEnded || player == null || enemy == null)
        {
            return;
        }

        if (currentTurn % 2 == 1)
        {
            waitingForPlayerCoinEffect = true;
            return;
        }

        waitingForPlayerCoinEffect = false;
        enemy.Act(player);

        if (CheckBattleOutcome())
        {
            return;
        }

        currentTurn++;
        ProcessTurn();
    }

    private bool CheckBattleOutcome()
    {
        if (enemy != null && enemy.IsDead)
        {
            isBattleEnded = true;
            waitingForPlayerCoinEffect = false;
            dungeonManager?.OnBattleWon();
            return true;
        }

        if (player != null && player.IsDead)
        {
            isBattleEnded = true;
            waitingForPlayerCoinEffect = false;
            EndGame();
            return true;
        }

        return false;
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }

}