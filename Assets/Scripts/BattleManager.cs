using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private DungeonManager dungeonManager;
    [SerializeField] private Player player;
    private Enemy enemy;
    private int currentTurn = 1;
    private CoinInventory coinInventory;
    public int CurrentTurn => currentTurn;

    void Start()
    {
        if (player != null) coinInventory = player.CoinInventory;
    }

    public void StartBattle(Enemy enemy)
    {
        this.enemy = enemy;
        currentTurn = 0;
        ProcessTurn();
    }

    private void ProcessTurn()
    {
        currentTurn++;
        if (IsPlayerTurn()) return;
        else ExecuteEnemyAction();
    }

    //コインのボタンにアタッチする
    public void ExecutePlayerCoinEffect(int coinIndex)
    {
        coinInventory.GetCoin(coinIndex).Effect(enemy);

       if(IsBattleContinued()) ProcessTurn();

    }

    private void ExecuteEnemyAction()
    {
        enemy.Act(player);
        if(IsBattleContinued()) ProcessTurn();
    }

    private bool IsBattleContinued()
    {
        if (enemy.IsDead)
        {
            dungeonManager?.OnBattleWon();
            return false;
        }

        if (player.IsDead)
        {
            EndGame();
            return false;
        }

        return true;
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
    private bool IsPlayerTurn()
    {
        return currentTurn % 2 == 1;
    }
}