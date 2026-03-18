using UnityEngine;

/// <summary>
/// Player character capable of attacking and healing.
/// </summary>
public class Player : Character
{
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Attacks the target for fixed damage.
    /// </summary>
    /// <param name="target">Character to attack</param>
    public void Attack(Character target)
    {
        target.TakeDamage(GameConstants.PlayerAttackDamage);
    }

    /// <summary>
    /// Restores health to this character.
    /// </summary>
    /// <param name="target">Not used (self-heal action)</param>
    public void Heal(Character target)
    {
        Heal(GameConstants.PlayerHealAmount);
    }
}
