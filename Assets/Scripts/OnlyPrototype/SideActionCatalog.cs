using System.Collections.Generic;
using UnityEngine;

public class SideActionCatalog : MonoBehaviour
{
    [System.Serializable]
    public class SideActionEntry
    {
        public Side side;
        public SideActionBase action;
    }

    [SerializeField] private List<SideActionEntry> entries = new List<SideActionEntry>();

    public SideActionBase GetActionFor(Side side)
    {
        if (side == null)
        {
            return null;
        }

        for (int i = 0; i < entries.Count; i++)
        {
            SideActionEntry entry = entries[i];
            if (entry != null && entry.side == side)
            {
                return entry.action;
            }
        }

        return null;
    }

    public IReadOnlyList<Side> GetAllSides()
    {
        List<Side> sides = new List<Side>();
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i] != null && entries[i].side != null)
            {
                sides.Add(entries[i].side);
            }
        }

        return sides;
    }

    public Side GetFirstSideByAction<TAction>() where TAction : SideActionBase
    {
        for (int i = 0; i < entries.Count; i++)
        {
            SideActionEntry entry = entries[i];
            if (entry == null || entry.side == null || entry.action == null)
            {
                continue;
            }

            if (entry.action is TAction)
            {
                return entry.side;
            }
        }

        return null;
    }
}
