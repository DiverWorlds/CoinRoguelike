using UnityEngine;

public class StaySide : BaseSide
{
    public override void Initialize()
    {
        frontOrBack = FrontAndBack.Back;
        effectName = "ミス";
        base.Initialize();
    }
    public override void Effect(int value, Character target)
    {
        target.TakeStay();
        //TODO: conversationWindow.Show("Bad! 1回休み！");
    }
}