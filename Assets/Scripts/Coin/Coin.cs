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

    public void SetSides(BaseSide frontSide, BaseSide backSide)
    {
        this.frontSide = frontSide;
        this.backSide = backSide;
    }

    public void Initialize(float frontSideProbability, int frontSideValue, int backSideValue)
    {
        this.frontSideProbability = frontSideProbability;
        this.frontSideValue = frontSideValue;
        this.backSideValue = backSideValue;
    }

    public void Effect(Character target)
    {
        if (Random.value <= FrontSideProbability)
        {
            frontSide.Effect(FrontSideValue, target);
        }
        else
        {
            backSide.Effect(BackSideValue, target);
        }
    }
}
