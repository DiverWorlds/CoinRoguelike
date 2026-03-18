using UnityEngine;

/// <summary>
/// Handles player movement animation when advancing stages.
/// Provides smooth acceleration/deceleration movement toward target positions.
/// </summary>
public class AdvanceCave : MonoBehaviour
{
    [SerializeField] private CaveManager caveManager;
    [SerializeField] private Transform player;

    private float targetX;
    private float currentSpeed;

    /// <summary>
    /// True if player is currently moving.
    /// </summary>
    public bool IsPlayerMoving { get; private set; }

    private void Start()
    {
        if (caveManager == null)
        {
            Debug.LogError("CaveManager is not assigned.", this);
            enabled = false;
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player is not assigned.", this);
            enabled = false;
            return;
        }

        targetX = player.position.x;
        currentSpeed = 0.0f;
    }

    private void Update()
    {
        if (player == null)
        {
            IsPlayerMoving = false;
            return;
        }

        float distance = targetX - player.position.x;
        if (Mathf.Abs(distance) <= GameConstants.DistanceThreshold && currentSpeed <= GameConstants.SpeedThreshold)
        {
            Vector3 snapPosition = player.position;
            snapPosition.x = targetX;
            player.position = snapPosition;
            currentSpeed = 0.0f;
            IsPlayerMoving = false;
            return;
        }

        IsPlayerMoving = true;

        float direction = Mathf.Sign(distance);
        float brakingDistance = (currentSpeed * currentSpeed) / Mathf.Max(2.0f * GameConstants.PlayerDeceleration, GameConstants.SpeedThreshold);

        if (Mathf.Abs(distance) <= brakingDistance)
        {
            currentSpeed = Mathf.Max(0.0f, currentSpeed - GameConstants.PlayerDeceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Min(GameConstants.MaxPlayerSpeed, currentSpeed + GameConstants.PlayerAcceleration * Time.deltaTime);
        }

        float moveX = direction * currentSpeed * Time.deltaTime;
        if (Mathf.Abs(moveX) > Mathf.Abs(distance))
        {
            moveX = distance;
            currentSpeed = 0.0f;
        }

        Vector3 nextPosition = player.position;
        nextPosition.x += moveX;
        player.position = nextPosition;
    }

    /// <summary>
    /// Initiates stage advance and sets player target position.
    /// </summary>
    public void AdvanceOneStage()
    {
        if (caveManager == null || player == null)
        {
            Debug.LogError("CaveManager or Player is not assigned.", this);
            return;
        }

        caveManager.AdvanceStage();
        targetX += GameConstants.StageDistancePerAdvance;
        IsPlayerMoving = true;
    }
}
