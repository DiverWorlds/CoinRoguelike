using System.Collections.Generic;
using UnityEngine;

public class SidesServer : MonoBehaviour
{
    [SerializeField] private List<BaseSide> sides;

    void Start()
    {
        }

    public BaseSide GetRandomSide()
    {
        int index = Random.Range(0, sides.Count);
        return sides[index];
    }
    public List<BaseSide> GetSides()
    {
        Debug.Log($"source: {sides[0].EffectName}, {sides[1].EffectName}");
        List<BaseSide> sidesCopy = new List<BaseSide>(sides);
        Debug.Log($"copied: {sidesCopy[0].EffectName}, {sidesCopy[1].EffectName}");
        return sidesCopy;
    }
}