using UnityEngine;

/// <summary>
/// Enemy character that attacks with a delay.
/// TODO: Implement custom turn order system in BattleManager for more flexible AI scheduling.
/// </summary>
public class Enemy : Character
{
    [SerializeField] private float attackDamage = 10f;

    public void ConfigureStats(float newMaxLife, float newAttackDamage)
    {
        maxLife = Mathf.Max(1f, newMaxLife);
        attackDamage = Mathf.Max(0f, newAttackDamage);
        currentLife = maxLife;
    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Initiates a delayed attack on the target.
    /// </summary>
    /// <param name="target">Character to attack</param>
    public void Attack(Character target)
    {
        StartCoroutine(AttackAfterDelay(target));
    }

    private System.Collections.IEnumerator AttackAfterDelay(Character target)
    {
        yield return new WaitForSeconds(GameConstants.EnemyAttackDelaySeconds);
        target.TakeDamage(attackDamage);
    }
}