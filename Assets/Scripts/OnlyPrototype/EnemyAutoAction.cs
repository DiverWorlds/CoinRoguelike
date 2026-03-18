using UnityEngine;

/// <summary>
/// Automatically determines and executes enemy turn actions.
/// Responds to turn changes detected via BattleManager.
/// </summary>
public class EnemyAutoAction : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;

    private int lastKnownTurn;

    private void Start()
    {
        if (battleManager == null)
        {
            Debug.LogError("BattleManager is not assigned.", this);
            enabled = false;
            return;
        }

        lastKnownTurn = battleManager.CurrentTurn;
    }

    private void Update()
    {
        int current = battleManager.CurrentTurn;
        if (current == lastKnownTurn)
        {
            return;
        }

        // Update lastKnownTurn first to prevent re-detection during AdvanceTurn
        lastKnownTurn = current;

        if (current % 2 == 0)
        {
            Enemy enemy = battleManager.EnemyCharacter as Enemy;
            if (enemy == null)
            {
                return;
            }

            enemy.TurnAction = enemy.Attack;
            battleManager.AdvanceTurn();
        }
    }
}
