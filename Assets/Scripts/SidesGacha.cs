using System.Collections.Generic;
using UnityEngine;

public class SidesGacha : MonoBehaviour
{
    [SerializeField] private List<BaseSide> sides;

    public BaseSide GetRandomSide()
    {
        int index = Random.Range(0, sides.Count);
        return sides[index];
    }
    public List<BaseSide> GetSides()
    {
        return new List<BaseSide>(sides);
    }
}