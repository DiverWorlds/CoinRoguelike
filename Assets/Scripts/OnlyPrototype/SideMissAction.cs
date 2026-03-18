using UnityEngine;

public class SideMissAction : SideActionBase
{
    protected override void ExecuteCore(Character actor, Character target, Side side, float effectPower)
    {
        string actorName = actor.gameObject.name;
        string sideName = side.SideName;
        Debug.Log($"{actorName} used {sideName}. Nothing happened.");
    }
}