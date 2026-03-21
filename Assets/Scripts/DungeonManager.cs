//TODO: 勝利時にSideのうちランダムで2つを、適切にパラメータを設定してInventoryに追加する機能追加
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
	[SerializeField] private SidesServer sidesServer;
	[SerializeField] private BattleManager battleManager;
	[SerializeField] private Player player;
	[SerializeField] private SideInventory sidesInventory;
	[SerializeField] private CallEnemiesManager callEnemiesManager;
	[SerializeField] private DungeonConstructor dungeonConstructor;
	[SerializeField] private float timeBetweenStages = 0.3f;
	private int currentStage = 1;
	private Enemy enemy;
	public int CurrentStage => currentStage;

	//SE関係
	[SerializeField] private GameObject seSpeaker;//SESpeakerのprefab
	[SerializeField] private AudioClip victorySE;

	void Start()
	{
		StartDungeon();
	}

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
		Destroy(enemy.gameObject);
		GameObject seSpeakerInstance = Instantiate(seSpeaker);
		seSpeakerInstance.GetComponent<SESpeaker>().Play(victorySE);
		sidesInventory.Add(sidesServer.GetRandomSide(currentStage));
		Invoke(nameof(StartNextStage), timeBetweenStages);
	}
	private bool StartNextStage()
	{
		currentStage++;
		Logger.Log("Call ProceedDungeon");
		dungeonConstructor.ProceedDungeon();
		StartBattle(currentStage);
		return true;
	}
}