using UnityEngine;

public class StaySide : BaseSide
{
    public override void Initialize(int currentStage, bool individualize)
    {
        frontOrBack = FrontAndBack.Back;
        effectName = "ミス";
        base.Initialize(currentStage, individualize);
    }
    public override void Effect(int value, Character target, Character user)
    {
        target.TakeStay();
        //TODO: conversationWindow.Show("Bad! 1回休み！");
    }
}