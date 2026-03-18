using UnityEngine;

public class Coin : MonoBehaviour
{
    private BaseSide frontSide;
    private BaseSide backSide;
    private float frontSideProbability;
    private int frontSideValue;
    private int backSideValue;

    public float FrontSideProbability => frontSideProbability;
    public int FrontSideValue => frontSideValue;
    public int BackSideValue => backSideValue;

    public void Initialize(BaseSide frontSide, BaseSide backSide)
    {
        this.frontSide = frontSide;
        this.backSide = backSide;
        frontSideProbability = CalcFrontSideProbability();
        frontSideValue = CalcFrontSideValue();
        backSideValue = CalcBackSideValue();
    }

    public void Effect(Character target)
    {
        if (FrontSideProbability <= Random.value)
        {
            frontSide.Effect(FrontSideValue, target);
        }
        else
        {
            backSide.Effect(BackSideValue, target);
        }
    }
    private float CalcFrontSideProbability()
    {
        //TODO: ちゃんと作る
        return 0.5f;
    }
    private int CalcFrontSideValue()
    {
        //TODO: ちゃんと作る
        return frontSide.Strength;
    }
    private int CalcBackSideValue()
    {
        //TODO: ちゃんと作る
        return backSide.Strength;
    }
}