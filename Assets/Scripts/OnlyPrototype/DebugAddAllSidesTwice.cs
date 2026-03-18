using UnityEngine;

public class DebugAddAllSidesTwice : MonoBehaviour
{
    [SerializeField] private PlayerSideInventory playerSideInventory;
    [SerializeField] private Side[] debugSides;

    public void AddAllSidesTwice()
    {
        if (playerSideInventory == null)
        {
            Debug.LogWarning("[DebugAddAllSidesTwice] PlayerSideInventory is not assigned.");
            return;
        }

        if (debugSides == null || debugSides.Length == 0)
        {
            Debug.LogWarning("[DebugAddAllSidesTwice] No debug sides assigned.");
            return;
        }

        for (int i = 0; i < debugSides.Length; i++)
        {
            Side side = debugSides[i];
            if (side == null)
            {
                continue;
            }

            playerSideInventory.AcquireSide(side);
            playerSideInventory.AcquireSide(side);
        }

        Debug.Log("[DebugAddAllSidesTwice] Added each configured side twice.");
    }
}
