//TODO: DungeonManager, BattleManagerのリファクタ
//TODO: 勝利時にSideのうちランダムで2つを、適切にパラメータを設定してInventoryに追加する機能追加
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
	private static class GameConstants
	{
		public const float DistanceThreshold = 0.001f;
		public const float SpeedThreshold = 0.001f;
		public const float PlayerAcceleration = 12.0f;
		public const float PlayerDeceleration = 14.0f;
		public const float MaxPlayerSpeed = 6.0f;
	}

    private class enemyPrefabData
    {
        public Enemies enemyName;
        public Enemy enemyPrefab;
    }
	[SerializeField] private BattleManager battleManager;
	[SerializeField] private Player player;
	[SerializeField] private List<enemyPrefabData> stageEnemyPrefabs = new List<enemyPrefabData>();
	[SerializeField] private int currentStage = 1;
    [SerializeField] private float stageMoveDistance = 1.0f;

    private float targetX;
    private float currentSpeed;

    public bool IsPlayerMoving { get; private set; }

	public int CurrentStage => currentStage;

	private void Update()
	{
		UpdatePlayerMovement();
	}

	public void StartDungeon()
	{
		currentStage = 1;
		if (player != null)
		{
			targetX = player.transform.position.x;
			currentSpeed = 0.0f;
			IsPlayerMoving = false;
		}
		StartCurrentStageBattle();
	}

	public bool StartNextStage()
	{
		currentStage++;
        MovePlayerToNextStagePosition();
		StartCurrentStageBattle();
		return true;
	}

	public void OnBattleWon()
	{
		player?.RecoverLifeOnBattleWin();
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

	private void MovePlayerToNextStagePosition()
	{
		if (player == null)
		{
			return;
		}

		targetX = player.transform.position.x + stageMoveDistance;
	}

	private void UpdatePlayerMovement()
	{
		if (player == null)
		{
			IsPlayerMoving = false;
			return;
		}

		Transform playerTransform = player.transform;
		float distance = targetX - playerTransform.position.x;
		if (Mathf.Abs(distance) <= GameConstants.DistanceThreshold && currentSpeed <= GameConstants.SpeedThreshold)
		{
			Vector3 snapPosition = playerTransform.position;
			snapPosition.x = targetX;
			playerTransform.position = snapPosition;
			currentSpeed = 0.0f;
			IsPlayerMoving = false;
			return;
		}

		IsPlayerMoving = true;

		float direction = Mathf.Sign(distance);
		float brakingDistance = (currentSpeed * currentSpeed) / Mathf.Max(2.0f * GameConstants.PlayerDeceleration, GameConstants.SpeedThreshold);

		if (Mathf.Abs(distance) <= brakingDistance)
		{
			currentSpeed = Mathf.Max(0.0f, currentSpeed - GameConstants.PlayerDeceleration * Time.deltaTime);
		}
		else
		{
			currentSpeed = Mathf.Min(GameConstants.MaxPlayerSpeed, currentSpeed + GameConstants.PlayerAcceleration * Time.deltaTime);
		}

		float moveX = direction * currentSpeed * Time.deltaTime;
		if (Mathf.Abs(moveX) > Mathf.Abs(distance))
		{
			moveX = distance;
			currentSpeed = 0.0f;
		}

		Vector3 nextPosition = playerTransform.position;
		nextPosition.x += moveX;
		playerTransform.position = nextPosition;
	}
}