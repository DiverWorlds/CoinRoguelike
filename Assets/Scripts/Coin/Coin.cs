using UnityEngine;

public class Coin : MonoBehaviour
{
    private BaseSide frontSide;
    private BaseSide backSide;
    private float frontSideProbability;
    private int frontSideValue;
    private int backSideValue;
    private float backSideBonus;
    private CointosEffect cointosEffect;

    public string FrontEffectName => frontSide.EffectName;
    public string BackEffectName => backSide.EffectName;
    public float FrontSideProbability => frontSideProbability;
    public int FrontSideValue => frontSideValue;
    public int BackSideValue => backSideValue;
    public float BackSideBonus => backSideBonus;//CoinFactoryですでにボーナス分が加算されているのでこれは参照用

    public void Initialize(BaseSide frontSide, BaseSide backSide, float frontSideProbability, int frontSideValue, int backSideValue, float backSideBonus)
    {
        this.frontSide = frontSide;
        this.backSide = backSide;
        this.frontSideProbability = frontSideProbability;
        this.frontSideValue = frontSideValue;
        this.backSideValue = backSideValue;
        this.backSideBonus = backSideBonus;
        cointosEffect = FindFirstObjectByType<CointosEffect>();
    }

    public void Effect(Character target, Character user)
    {
        if (Random.value <= FrontSideProbability)
        {
            cointosEffect.ResultFront();
            frontSide.Effect(FrontSideValue, target, user);
        }
        else
        {
            cointosEffect.ResultBack();
            backSide.Effect(BackSideValue, target, user);
        }
    }
}
