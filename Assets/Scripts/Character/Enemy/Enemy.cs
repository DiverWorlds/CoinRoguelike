using UnityEngine;

//TODO: Enemyにsleep時の処理を追加
abstract public class Enemy : Character
{
    [SerializeField] protected int power;


    public void Initialize(int maxLife, int power)
    {
        MaxLife = maxLife;
        this.power = power;
    }

    // 行動ルーティンを定義する
    public virtual void Act(Player player)
    {
    }
    protected void Attack(Character target, int damage)
    {
        target.TakeDamage(damage);
    }
}