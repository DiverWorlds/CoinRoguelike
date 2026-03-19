using UnityEngine;

public class Coin : MonoBehaviour
{
    private BaseSide frontSide;
    private BaseSide backSide;
    private float frontSideProbability;
    private int frontSideValue;
    private int backSideValue;

    public string FrontEffectName => frontSide.EffectName;
    public string BackEffectName => backSide.EffectName;
    public float FrontSideProbability => frontSideProbability;
    public int FrontSideValue => frontSideValue;
    public int BackSideValue => backSideValue;

    public void Initialize(BaseSide frontSide, BaseSide backSide, float frontSideProbability, int frontSideValue, int backSideValue)
    {
        Debug.Log($"Coin.Initialize() called: frontSide={frontSide?.EffectName ?? "NULL"}, backSide={backSide?.EffectName ?? "NULL"}");
        this.frontSide = frontSide;
        this.backSide = backSide;
        this.frontSideProbability = frontSideProbability;
        this.frontSideValue = frontSideValue;
        this.backSideValue = backSideValue;
        Debug.Log($"Coin.Initialize() completed: FrontEffectName={FrontEffectName}, BackEffectName={BackEffectName}");
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
