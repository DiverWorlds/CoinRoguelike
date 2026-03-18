using UnityEngine;

/// <summary>
/// Manages the turn-based battle system.
/// Handles player and enemy turn execution, enemy defeat flow, and game over conditions.
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] private Character playerCharacter;
    [SerializeField] private Character enemyCharacter;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private AdvanceCave advanceCave;
    [SerializeField] private PlayerSideInventory playerSideInventory;
    [SerializeField] [Range(0.0f, 1.0f)] private float playerHealRateOnEnemyDefeat = 0.2f;

    public static BattleManager Instance { get; private set; }

    public int CurrentTurn { get; private set; }
    public int CurrentStage { get; private set; } = 1;

    public Character PlayerCharacter => playerCharacter;
    public Character EnemyCharacter => enemyCharacter;

    private bool isBattleEnded;
    private bool isResolvingEnemyDefeat;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple BattleManager instances found. Using the latest instance.");
        }

        Instance = this;
        StartBattle();
    }

    public void StartBattle()
    {
        CurrentTurn = 1;

        if (enemyCharacter == null)
        {
            SpawnEnemyForStage(CurrentStage, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (isBattleEnded)
        {
            return;
        }

        if (playerCharacter != null && playerCharacter.CurrentLife <= 0)
        {
            EndGame();
            return;
        }

        if (advanceCave != null && advanceCave.IsPlayerMoving)
        {
            return;
        }

        if (!isResolvingEnemyDefeat && enemyCharacter != null && enemyCharacter.CurrentLife <= 0)
        {
            StartCoroutine(HandleEnemyDefeat());
        }
    }

    /// <summary>
    /// Advances the turn: executes player or enemy action based on CurrentTurn.
    /// Odd turns -> Player acts, Even turns -> Enemy acts.
    /// </summary>
    public void AdvanceTurn()
    {
        if (isBattleEnded || isResolvingEnemyDefeat)
        {
            return;
        }

        if (advanceCave != null && advanceCave.IsPlayerMoving)
        {
            return;
        }

        if (playerCharacter == null || enemyCharacter == null)
        {
            Debug.LogWarning("BattleManager needs both playerCharacter and enemyCharacter references.");
            return;
        }

        if (CurrentTurn % 2 == 1)
        {
            playerCharacter.TurnAction?.Invoke(enemyCharacter);
        }
        else
        {
            enemyCharacter.TurnAction?.Invoke(playerCharacter);
        }

        CurrentTurn++;
    }

    public void CompletePlayerActionAndAdvanceToEnemyTurn()
    {
        if (isBattleEnded || isResolvingEnemyDefeat)
        {
            return;
        }

        if (advanceCave != null && advanceCave.IsPlayerMoving)
        {
            return;
        }

        if (CurrentTurn % 2 == 1)
        {
            CurrentTurn++;
        }
    }

    /// <summary>
    /// Gets the current HP summary for both player and enemy.
    /// </summary>
    /// <returns>Formatted string like "Player: 50, Enemy: 30"</returns>
    public string GetCurrentLifeSummary()
    {
        if (playerCharacter == null || enemyCharacter == null)
        {
            return "Player: --, Enemy: --";
        }

        int playerLife = Mathf.Max(0, Mathf.RoundToInt(playerCharacter.CurrentLife));
        int enemyLife = Mathf.Max(0, Mathf.RoundToInt(enemyCharacter.CurrentLife));
        return $"Player: {playerLife:00}, Enemy: {enemyLife:00}";
    }

    /// <summary>
    /// Handles the enemy defeat flow: delay, stage advance, and enemy respawn.
    /// </summary>
    private System.Collections.IEnumerator HandleEnemyDefeat()
    {
        isResolvingEnemyDefeat = true;
        ApplyEnemyDefeatRewards();

        GameObject defeatedEnemy = enemyCharacter != null ? enemyCharacter.gameObject : null;
        Quaternion spawnRotation = defeatedEnemy != null ? defeatedEnemy.transform.rotation : Quaternion.identity;

        DestroyDefeatedEnemy(defeatedEnemy);
        yield return WaitForEnemyRespawnDelay();
        yield return AdvanceStageWithPlayerMovement();
        SpawnNewEnemy(spawnRotation);

        isResolvingEnemyDefeat = false;
    }

    private void ApplyEnemyDefeatRewards()
    {
        if (playerCharacter != null)
        {
            float healAmount = playerCharacter.MaxLife * playerHealRateOnEnemyDefeat;
            playerCharacter.Heal(healAmount);
        }

        PlayerSideInventory inventory = playerSideInventory;
        if (inventory == null && playerCharacter != null)
        {
            inventory = playerCharacter.GetComponent<PlayerSideInventory>();
        }

        if (inventory != null)
        {
            inventory.AcquireRandomSideFromCatalog(CurrentStage);
        }
        else
        {
            Debug.LogWarning("BattleManager could not grant enemy-defeat side reward because PlayerSideInventory was not found.");
        }
    }

    private void DestroyDefeatedEnemy(GameObject defeatedEnemy)
    {
        if (defeatedEnemy != null)
        {
            Destroy(defeatedEnemy);
        }

        enemyCharacter = null;
    }

    private System.Collections.IEnumerator WaitForEnemyRespawnDelay()
    {
        yield return new WaitForSeconds(GameConstants.EnemyRespawnDelaySeconds);
    }

    private System.Collections.IEnumerator AdvanceStageWithPlayerMovement()
    {
        if (advanceCave != null)
        {
            advanceCave.AdvanceOneStage();

            while (advanceCave.IsPlayerMoving)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("AdvanceCave is not assigned on BattleManager.");
        }
    }

    private void SpawnNewEnemy(Quaternion spawnRotation)
    {
        int spawnedStage = CurrentStage + 1;
        SpawnEnemyForStage(spawnedStage, spawnRotation);
    }

    private void SpawnEnemyForStage(int stage, Quaternion spawnRotation)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned on BattleManager.");
            return;
        }

        Vector3 spawnPosition = playerCharacter != null
            ? playerCharacter.transform.position + new Vector3(GameConstants.EnemySpawnOffsetX, 0.0f, 0.0f)
            : Vector3.zero;

        Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        GetEnemyStatsForStage(stage, out float enemyMaxLife, out float enemyAttackDamage);
        newEnemy.ConfigureStats(enemyMaxLife, enemyAttackDamage);

        enemyCharacter = newEnemy;
        CurrentTurn = 1;
        CurrentStage = stage;
    }

    private void GetEnemyStatsForStage(int stage, out float maxLife, out float attackDamage)
    {
        if (stage <= 10)
        {
            maxLife = 15f;
            attackDamage = 10f;
            return;
        }

        if (stage <= 20)
        {
            maxLife = 30f;
            attackDamage = 15f;
            return;
        }

        if (stage <= 30)
        {
            maxLife = 90f;
            attackDamage = 25f;
            return;
        }

        if (stage <= 40)
        {
            maxLife = 250f;
            attackDamage = 40f;
            return;
        }

        maxLife = 350f;
        attackDamage = 60f;
    }

    private void EndGame()
    {
        isBattleEnded = true;
        Debug.Log("Player HP reached 0. Game Over.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
