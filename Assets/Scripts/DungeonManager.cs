//TODO: 勝利時にSideのうちランダムで2つを、適切にパラメータを設定してInventoryに追加する機能追加
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private SidesServer sidesServer;
	[SerializeField] private BattleManager battleManager;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private Player player;
    [SerializeField] private SideInventory sidesInventory;
	[SerializeField] private int currentStage = 1;
    [SerializeField] private CallEnemiesManager callEnemiesManager;
    private Enemy enemy;
	public int CurrentStage => currentStage;
	public bool IsPlayerMoving => playerMovement != null && playerMovement.IsPlayerMoving;

	public void StartDungeon()
	{
		currentStage = 1;
		StartBattle(currentStage);
	}
	private void StartBattle(int stage)
	{
		enemy = callEnemiesManager.InstantiateEnemyPrefab(stage);
		battleManager.StartBattle(enemy);
	}
    
    public void OnBattleWon()
	{
        Logger.Log("OnBattleWon called");
		player.RecoverLife();
        Logger.Log("sidesInventory", sidesInventory);
        Logger.Log("sidesServer", sidesServer);
        sidesInventory.Add(sidesServer.GetRandomSide());
		StartNextStage();
	}
    private bool StartNextStage()
	{
		currentStage++;
		playerMovement?.Advance();
		StartBattle(currentStage);
		return true;
	}
}