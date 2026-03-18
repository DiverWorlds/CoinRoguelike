//TODO: DungeonManager, BattleManagerのリファクタ
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
	[SerializeField] private Player player;
	[SerializeField] private List<enemyPrefabData> stageEnemyPrefabs = new List<enemyPrefabData>();
	[SerializeField] private int currentStage = 1;

	public int CurrentStage => currentStage;

	public void StartDungeon()
	{
		currentStage = 1;
		StartCurrentStageBattle();
	}

	public bool StartNextStage()
	{
		currentStage++;
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
		Enemy enemy = Instantiate(stageEnemyPrefabs.Where(x => x.enemyName == Enemies.Mimic).Select(x => x.enemyPrefab).FirstOrDefault());
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