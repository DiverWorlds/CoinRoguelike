using UnityEngine;

/// <summary>
/// Handles UI button inputs for player battle actions.
/// Forwards player decisions to BattleManager for turn execution.
/// </summary>
public class PlayerBattleActions : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BattleManager battleManager;

    /// <summary>
    /// Called when Attack button is clicked.
    /// </summary>
    public void SubmitAttack()
    {
        player.TurnAction = player.Attack;
        battleManager.AdvanceTurn();
    }

    /// <summary>
    /// Called when Heal button is clicked.
    /// </summary>
    public void SubmitHeal()
    {
        player.TurnAction = player.Heal;
        battleManager.AdvanceTurn();
    }
}
