using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SidesServer : MonoBehaviour
{
    [SerializeField] private List<BaseSide> sides;
    [SerializeField] private SideInventory sideInventory;

    void Start()
    {
        }

    public BaseSide GetRandomSide()
    {
        Logger.LogElements("sides", sides.Select(side => side.EffectName));
        Logger.Log("sideInventory", sideInventory);
        int index = Random.Range(0, sides.Count);
        Logger.Log("sides.Count", sides.Count);
        BaseSide side = sides[index];
        Logger.Log("Selected side", side.EffectName);
        BaseSide newSide = Instantiate(side.gameObject, sideInventory.transform).GetComponent<BaseSide>();
        Logger.Log("Instantiated new side's place", newSide.transform.parent.name);
        newSide.Initialize();
        return newSide;
    }
    public List<BaseSide> GetSides()
    {
        return sides;
    }
}