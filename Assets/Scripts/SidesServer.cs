using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SidesServer : MonoBehaviour
{
    [SerializeField] private DropDisplays dropDisplays;
    [SerializeField] private List<BaseSide> sides;
    [SerializeField] private SideInventory sideInventory;

    public BaseSide GetRandomSide(int currentStage)
    {
        Logger.LogElements("sides", sides.Select(side => side.EffectName));
        Logger.Log("sideInventory", sideInventory);
        int index = Random.Range(0, sides.Count);
        Logger.Log("sides.Count", sides.Count);
        BaseSide side = sides[index];
        Logger.Log("Selected side", side.EffectName);
        BaseSide newSide = Instantiate(side.gameObject, sideInventory.transform).GetComponent<BaseSide>();
        Logger.Log("Instantiated new side's place", newSide.transform.parent.name);
        newSide.Initialize(currentStage, true);
        dropDisplays.CreateDropDisplays(newSide);
        return newSide;
    }

    public BaseSide GetSpecificSide(int currentStage, string effectName)
    {
        BaseSide side = null;
        foreach (BaseSide search in sides)
        {
            search.Initialize(currentStage, false);//個体差なしで初期化してから効果名を比較
            if (search.EffectName == effectName) side = search;
        }
        if (side == null)
        {
            Logger.Log($"No side found with effect name: {effectName}");
            return null;
        }

        BaseSide newSide = Instantiate(side.gameObject, sideInventory.transform).GetComponent<BaseSide>();
        newSide.Initialize(currentStage, false);//個体差なしで初期化
        Logger.Log(effectName + "を個体値なしで生成しました");
        return newSide;
    }
    public List<BaseSide> GetSides()
    {
        return sides;
    }
}