using UnityEngine;

public class EnemyParent : Singleton<EnemyParent>
{
    private Enemy enemy;
    public Enemy Enemy => enemy;
    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        if (enemy == null)
        {
            return;
        }

        enemy.transform.SetParent(transform, false);
    }
}