using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideInventory : MonoBehaviour
{
    private List<BaseSide> sides = new List<BaseSide>();


    public void Add(BaseSide side)
    {
        sides.Add(side);
    }
    public void Remove(BaseSide side)
    {
        sides.Remove(side);
    }

    public List<BaseSide> GetByFrontOrBack(FrontAndBack frontOrBack)
    {
        return sides.Where(side => side.FrontOrBack == frontOrBack).ToList();
    }

    public List<BaseSide> GetSortedByFrontBackAndStrength(bool isStrengthAscending)
    {
        return isStrengthAscending
            ? sides.OrderBy(side => side.FrontOrBack).ThenBy(side => side.Strength).ToList()
            : sides.OrderBy(side => side.FrontOrBack).ThenByDescending(side => side.Strength).ToList();
    }

    public List<BaseSide> GetSortedByFrontBackAndWeight(bool isWeightAscending)
    {
        return isWeightAscending
            ? sides.OrderBy(side => side.FrontOrBack).ThenBy(side => side.Weight).ToList()
            : sides.OrderBy(side => side.FrontOrBack).ThenByDescending(side => side.Weight).ToList();
    }
}