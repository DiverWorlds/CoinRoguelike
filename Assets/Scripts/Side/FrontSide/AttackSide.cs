using UnityEngine;

public class AttackSide : BaseSide
{
    public override void Initialize()
    {
        frontOrBack = FrontAndBack.Front;
        effectName = "攻撃";
        base.Initialize();
    }
    public override void Effect(int value, Character target)
    {
        target.TakeDamage(value);
        //TODO: conversationWindow.Show("Good! {damage}の攻撃！");
    }
}