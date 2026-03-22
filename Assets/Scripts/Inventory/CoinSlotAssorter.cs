using UnityEngine;
using System.Collections.Generic;

public class CoinSlotAssorter : MonoBehaviour
{
    [SerializeField] private List<Transform> coinSlots;

    public bool AssignToEmptySlot(Transform item)
    {
        if (item == null)
        {
            Debug.LogWarning("AssignToEmptySlot failed: target is null.");
            return false;
        }

        foreach (Transform slot in coinSlots)
        {
            if (slot == null)
            {
                continue;
            }

            if (slot.childCount > 0)
            {
                continue;
            }

            item.SetParent(slot, false);
            item.localPosition = Vector3.zero;
            item.localRotation = Quaternion.identity;
            return true;
        }

        Debug.LogWarning("AssignToEmptySlot failed: no empty slot found.");
        return false;
    }
}