using UnityEngine;

public class DrainSide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Front;
        effectName = "吸血";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target.TakeDamage(value);
        user.TakeHeal(value);
    }
}