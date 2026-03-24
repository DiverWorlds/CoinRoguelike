using UnityEngine;

public class AttackSide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Front;
        effectName = "攻撃";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target.TakeDamage(value);
        //TODO: conversationWindow.Show("Good! {damage}の攻撃！");
    }
}