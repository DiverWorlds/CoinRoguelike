using UnityEngine;

public class HeavyAttackSide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Front;
        effectName = "重撃";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target.TakeDamage(value);
    }
}