//TODO: DungeonManager, BattleManagerのリファクタ
//TODO: 勝利時にSideのうちランダムで2つを、適切にパラメータを設定してInventoryに追加する機能追加
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private class enemyPrefabData
    {
        public Enemies enemyName;
        public Enemy enemyPrefab;
    }
	[SerializeField] private BattleManager battleManager;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private Player player;
	[SerializeField] private List<enemyPrefabData> stageEnemyPrefabs = new List<enemyPrefabData>();
	[SerializeField] private int currentStage = 1;
    [SerializeField] private float stageMoveDistance = 1.0f;

	public int CurrentStage => currentStage;
	public bool IsPlayerMoving => playerMovement != null && playerMovement.IsPlayerMoving;

	public void StartDungeon()
	{
		currentStage = 1;
		StartCurrentStageBattle();
	}

	public bool StartNextStage()
	{
		currentStage++;
		playerMovement?.Advance(stageMoveDistance);
		StartCurrentStageBattle();
		return true;
	}

	public void OnBattleWon()
	{
		player?.RecoverLife();
		StartNextStage();
	}

	private void StartCurrentStageBattle()
	{
		if (battleManager == null)
		{
			return;
		}

		Enemy enemy = CreateEnemyForCurrentStage();
		if (enemy == null)
		{
			return;
		}

		battleManager.SetEnemy(enemy);
		battleManager.StartBattle();
	}

	private Enemy CreateEnemyForCurrentStage()
	{
		if (stageEnemyPrefabs == null || stageEnemyPrefabs.Count == 0)
		{
			return null;
		}

        //TODO: 仮実装。ステージ数に応じた敵の選択ロジックを実装する必要がある
		Enemy enemyPrefab = stageEnemyPrefabs.Where(x => x.enemyName == Enemies.Mimic).Select(x => x.enemyPrefab).FirstOrDefault();
		if (enemyPrefab == null)
		{
			return null;
		}

		Vector3 spawnPosition = enemyPrefab.transform.position;
		if (player != null)
		{
			Vector3 playerPosition = player.transform.position;
			spawnPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + enemyPrefab.EnemyOffset);
		}

		Enemy enemy = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
		if (enemy == null)
		{
			return null;
		}
		enemy.Initialize(CalcMimicMaxLife(), CalcMimicPower());

		return enemy;
	}
    private int CalcMimicMaxLife()
    {
        // enemyMaxLife = round(15 * 2^((max(1,currentStage)-1)/10))
        //TODO: ブレは求めた最大HPの5%ほどにする？
        //TODO: Enemy.Powerが関係なくなっている可能性がある
/*
stage 1: 15
stage 11: 30
stage 21: 60
stage 31: 120
stage 41: 240
stage 51: 480
stage 61: 960
stage 71: 1920
stage 81: 3840
stage 91: 7680
*/
		float x = Mathf.Max(1, currentStage);
		float growthMultiplier = Mathf.Pow(2f, (x - 1f) / 10f);
		return Mathf.RoundToInt(15f * growthMultiplier);
	}

	private int CalcMimicPower()
	{
        // enemyPower = clamp(round(500 - 490exp(-0.0042(max(1,currentStage)-1))), 10, 500)
/*
stage 1: 10
stage 11: 30
stage 21: 49
stage 31: 68
stage 41: 86
stage 51: 103
stage 61: 119
stage 71: 135
stage 81: 150
stage 91: 164
*/
		float x = Mathf.Max(1, currentStage);
				float power = 500f - 490f * Mathf.Exp(-0.0042f * (x - 1f));
		return Mathf.Clamp(Mathf.RoundToInt(power), 10, 500);
    }

}