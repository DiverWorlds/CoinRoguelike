using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static class Constants
    {
        public const float DistanceThreshold = 0.001f;
        public const float SpeedThreshold = 0.001f;
        public const float PlayerAcceleration = 12.0f;
        public const float PlayerDeceleration = 14.0f;
        public const float MaxPlayerSpeed = 6.0f;
    }
    [SerializeField] private Player player;
    private Transform playerTransform;
    private float targetZ;
    private float currentSpeed;

    public bool IsPlayerMoving { get; private set; }

    void Start()
    {
        if (player == null)
        {
            playerTransform = null;
            currentSpeed = 0.0f;
            IsPlayerMoving = false;
            return;
        }

        playerTransform = transform;
        targetZ = playerTransform.position.z;
        currentSpeed = 0.0f;
        IsPlayerMoving = false;
    }

    private void Update()
    {
        UpdatePlayerMovement();
    }

    public void Advance(float stageMoveDistance)
    {
        Debug.Log($"PlayerMovement: Advance called with stageMoveDistance={stageMoveDistance}");
        if (playerTransform == null)
        {
            return;
        }

        targetZ = playerTransform.position.z + stageMoveDistance;
    }

    private void UpdatePlayerMovement()
    {
        if (playerTransform == null)
        {
            IsPlayerMoving = false;
            return;
        }

        float distance = targetZ - playerTransform.position.z;
        if (Mathf.Abs(distance) <= Constants.DistanceThreshold && currentSpeed <= Constants.SpeedThreshold)
        {
            Vector3 snapPosition = playerTransform.position;
            snapPosition.z = targetZ;
            playerTransform.position = snapPosition;
            currentSpeed = 0.0f;
            IsPlayerMoving = false;
            return;
        }

        IsPlayerMoving = true;
        Debug.Log($"PlayerMovement: UpdatePlayerMovement called. CurrentZ={playerTransform?.position.z}, TargetZ={targetZ}, CurrentSpeed={currentSpeed}");


        float direction = Mathf.Sign(distance);
        float brakingDistance = (currentSpeed * currentSpeed) / Mathf.Max(2.0f * Constants.PlayerDeceleration, Constants.SpeedThreshold);

        if (Mathf.Abs(distance) <= brakingDistance)
        {
            currentSpeed = Mathf.Max(0.0f, currentSpeed - Constants.PlayerDeceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Min(Constants.MaxPlayerSpeed, currentSpeed + Constants.PlayerAcceleration * Time.deltaTime);
        }

        float moveZ = direction * currentSpeed * Time.deltaTime;
        if (Mathf.Abs(moveZ) > Mathf.Abs(distance))
        {
            moveZ = distance;
            currentSpeed = 0.0f;
        }

        Vector3 nextPosition = playerTransform.position;
        nextPosition.z += moveZ;
        playerTransform.position = nextPosition;
    }
}
