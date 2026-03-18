using UnityEngine;

[System.Serializable]
public class Coin
{
    public Side FrontSide { get; }
    public Side BackSide { get; }

    public float FrontProbability { get; }
    public float BackProbability { get; }

    public float FrontBackCoefficient { get; }

    public float FrontPerformanceValue =>
        FrontSide.Value * ((1.0f + BackProbability) * FrontBackCoefficient);

    public float BackPerformanceValue =>
        BackSide.Value * ((1.0f + FrontProbability) * FrontBackCoefficient);

    public Coin(Side frontSide, Side backSide, float frontBackCoefficient = 1.0f)
    {
        FrontSide = frontSide;
        BackSide = backSide;
        FrontBackCoefficient = frontBackCoefficient;

        int normalizedFrontWeight = Mathf.Max(1, frontSide.Weight);
        int normalizedBackWeight = Mathf.Max(1, backSide.Weight);

        float inverseFrontWeight = 1.0f / normalizedFrontWeight;
        float inverseBackWeight = 1.0f / normalizedBackWeight;
        float total = inverseFrontWeight + inverseBackWeight;

        FrontProbability = inverseFrontWeight / total;
        BackProbability = inverseBackWeight / total;
    }

    public FrontAndBack Toss()
    {
        return Random.value < FrontProbability ? FrontAndBack.Front : FrontAndBack.Back;
    }

    public Side TossSide()
    {
        return Toss() == FrontAndBack.Front ? FrontSide : BackSide;
    }

    public float GetPerformanceValueFor(Side side)
    {
        if (side == FrontSide)
        {
            return FrontPerformanceValue;
        }

        if (side == BackSide)
        {
            return BackPerformanceValue;
        }

        return side != null ? side.Value : 0f;
    }
}