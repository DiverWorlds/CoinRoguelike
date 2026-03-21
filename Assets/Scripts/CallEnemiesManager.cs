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
    [SerializeField] private float enemyOffset = 20f;
    [SerializeField] private float initialEnemyOffset = 2f;
    [SerializeField] private Vector3 enemySpawnOffset;

    public Enemy InstantiateEnemyPrefab(int stage)
    {
        EnemyPrefabData enemyData;
        if (stage % 10 == 0)//10の倍数のステージでは、通常よりも強い敵（例: ボスミミック）を出現させるロジック。
        {
            int randomValue = UnityEngine.Random.Range(0, 3);
            if (randomValue == 0 || stage == 10)
            {
                enemyData = enemyPrefabs.Find(x => x.enemyName == Enemies.BossMimic);//ボスミミック出現
            }
            else if (randomValue == 1)
            {
                enemyData = enemyPrefabs.Find(x => x.enemyName == Enemies.Spider);//クモ出現
            }
            else
            {
                enemyData = enemyPrefabs.Find(x => x.enemyName == Enemies.Dragon);//ドラゴン出現
            }
        }
        else//通常の敵であるミミックを出現させるロジック。
        {
            enemyData = enemyPrefabs.Find(x => x.enemyName == Enemies.Mimic);//ミミック出現
        }

        Enemy enemy = Instantiate(enemyData.enemyPrefab);
        EnemyParent.Instance.SetEnemy(enemy);
        Vector3 newPosition = player.position + new Vector3(0f, 0f, CalculateEnemySpawnOffset(stage)) + enemySpawnOffset; ;
        enemy.transform.position = newPosition;
        enemy.Initialize(stage);
        return enemy;
    }

    private float CalculateEnemySpawnOffset(int stage)
    {
        return stage == 1 ? initialEnemyOffset : enemyOffset;
    }
}