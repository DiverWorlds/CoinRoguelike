using UnityEngine;

public class SideAttackAction : SideActionBase
{
    protected override void ExecuteCore(Character actor, Character target, Side side, float effectPower)
    {
        if (target == null)
        {
            return;
        }

        target.TakeDamage(effectPower);
    }
}