using UnityEngine;

public class SideMisfireAction : SideActionBase
{
    protected override void ExecuteCore(Character actor, Character target, Side side, float effectPower)
    {
        actor.TakeDamage(effectPower);
    }
}