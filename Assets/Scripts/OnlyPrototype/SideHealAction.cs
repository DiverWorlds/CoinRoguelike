using UnityEngine;

public class SideHealAction : SideActionBase
{
    protected override void ExecuteCore(Character actor, Character target, Side side, float effectPower)
    {
        actor.Heal(effectPower);
    }
}