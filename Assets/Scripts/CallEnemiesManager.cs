using UnityEngine;
using System.Collections.Generic;
using System;

public class CallEnemiesManager : MonoBehaviour
{
    [Serializable]
    private class EnemyPrefabData
    {
        public Enemies enemyName;
        public Enemy enemyPrefab;
    }
	[SerializeField] private List<EnemyPrefabData> enemyPrefabs = new List<EnemyPrefabData>();
    [SerializeField] private Transform player;
    [SerializeField] private float enemyOffset = 8f;
    [SerializeField] private float initialEnemyOffset = 2f;

    public Enemy InstantiateEnemyPrefab(int stage)
    {
        //TODO: ミミックしか出ないのでロジックを書く
        //TODO: コメントわかりやすく
        var enemyData = enemyPrefabs.Find(x => x.enemyName == Enemies.Mimic);
        Enemy enemy = Instantiate(enemyData.enemyPrefab);
        Vector3 newPosition = player.position + new Vector3(0f, 0f, stage==1 ? initialEnemyOffset : enemyOffset);
        enemy.transform.position = newPosition;
        enemy.Initialize(CalcMimicMaxLife(stage), CalcMimicPower(stage));
        return enemy;
    }
    private int CalcMimicMaxLife(int stage)
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
		float x = Mathf.Max(1, stage);
		float growthMultiplier = Mathf.Pow(2f, (x - 1f) / 10f);
		return Mathf.RoundToInt(15f * growthMultiplier);
	}

	private int CalcMimicPower(int stage)
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
		float x = Mathf.Max(1, stage);
				float power = 500f - 490f * Mathf.Exp(-0.0042f * (x - 1f));
		return Mathf.Clamp(Mathf.RoundToInt(power), 10, 500);
    }
}