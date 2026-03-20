using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideInventory : MonoBehaviour
{
    private List<BaseSide> sides = new List<BaseSide>();


    public void Add(BaseSide side)
    {
        Logger.Log($"Add Side: {side.EffectName}");
        sides.Add(side);
    }
    public void Remove(BaseSide side)
    {
        sides.Remove(side);
    }

    public List<BaseSide> GetSortedByStrength(FrontAndBack frontOrBack, bool isAscending)
    {
        IEnumerable<BaseSide> targetSides = sides.Where(side => side.FrontOrBack == frontOrBack);

        return isAscending
            ? targetSides.OrderBy(side => side.Strength).ToList()
            : targetSides.OrderByDescending(side => side.Strength).ToList();
    }

    public List<BaseSide> GetSortedByWeight(FrontAndBack frontOrBack, bool isAscending)
    {
        IEnumerable<BaseSide> targetSides = sides.Where(side => side.FrontOrBack == frontOrBack);

        return isAscending
            ? targetSides.OrderBy(side => side.Weight).ToList()
            : targetSides.OrderByDescending(side => side.Weight).ToList();
    }
    public List<BaseSide> GetByFrontOrBack(FrontAndBack frontOrBack)
    {
        Logger.LogElements("sides", sides.Select(s => $"{s.EffectName}({s.FrontOrBack})"));
        return sides.Where(side => side.FrontOrBack == frontOrBack).ToList();
    }

}