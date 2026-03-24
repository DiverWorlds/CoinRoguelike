using UnityEngine;

public class BigAccidentSide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Back;
        effectName = "大暴発";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target = user;//自分にダメージ
        target.TakeDamage(value);
    }
}