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
        return sides;
    }
}