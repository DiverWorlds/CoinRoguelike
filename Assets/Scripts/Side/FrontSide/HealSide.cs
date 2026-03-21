using UnityEngine;

public class HealSide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Front;
        effectName = "回復";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target = user; //回復は自分にかける
        target.TakeHeal(value);
    }
}