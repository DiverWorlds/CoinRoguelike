using System;
using UnityEngine;

/// <summary>
/// Base class for all battle participants (Player, Enemy).
/// Handles HP tracking, damage calculation, and turn actions.
/// </summary>
public class Character : MonoBehaviour
{ 
    [SerializeField] protected float maxLife;
    protected float currentLife;

    /// <summary>
    /// Current HP of this character (read-only).
    /// </summary>
    public float CurrentLife => currentLife;

    /// <summary>
    /// Maximum HP of this character (read-only).
    /// </summary>
    public float MaxLife => maxLife;

    /// <summary>
    /// Action delegate to be invoked when it's this character's turn.
    /// Receiver should execute their combat action.
    /// </summary>
    public Action<Character> TurnAction;

    protected virtual void Start()
    {
        currentLife = maxLife;
    }

    /// <summary>
    /// Applies damage to this character and logs the result with battle HP summary.
    /// </summary>
    /// <param name="damage">Damage amount to apply</param>
    public void TakeDamage(float damage)
    {
        currentLife = Mathf.Max(0, currentLife - damage);

        BattleManager battleManager = BattleManager.Instance;
        if (battleManager == null)
        {
            Debug.Log($"{gameObject.name} took {damage} damage.");
            return;
        }

        Debug.Log($"{gameObject.name} took {damage} damage. {battleManager.GetCurrentLifeSummary()}");
    }

    public void Heal(float amount)
    {
        currentLife = Mathf.Min(currentLife + amount, maxLife);
        Debug.Log($"{gameObject.name} healed {amount}. Current life: {currentLife}");
    }
}