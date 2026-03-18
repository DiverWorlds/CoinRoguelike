using UnityEngine;

public static class GameConstants
{
    // Character Stats
    public const float PlayerMaxHealth = 100f;
    public const float PlayerAttackDamage = 10f;
    public const float PlayerHealAmount = 10f;

    public const float EnemyAttackDamage = 10f;
    public const float EnemyAttackDelaySeconds = 1.0f;

    // Battle Flow
    public const float EnemyRespawnDelaySeconds = 0.5f;
    public const float EnemySpawnOffsetX = 3.0f;

    // Movement
    public const float StageDistancePerAdvance = 3.0f;
    public const float MaxPlayerSpeed = 6.0f;
    public const float PlayerAcceleration = 12.0f;
    public const float PlayerDeceleration = 14.0f;

    // UI
    public const string DefaultHealthLabel = "myHP";

    // Debug
    public const float DistanceThreshold = 0.001f;
    public const float SpeedThreshold = 0.001f;
}
