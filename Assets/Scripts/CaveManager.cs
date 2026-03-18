using UnityEngine;

/// <summary>
/// Manages cave progression (stage tracking).
/// </summary>
public class CaveManager : MonoBehaviour
{
    [SerializeField] private int currentStage = 1;

    /// <summary>
    /// Current stage number.
    /// </summary>
    public int CurrentStage => currentStage;

    /// <summary>
    /// Advances to the next stage.
    /// </summary>
    public void AdvanceStage()
    {
        currentStage++;
    }
}
