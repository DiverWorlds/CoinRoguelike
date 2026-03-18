using UnityEngine;

public static class CoinFactory
{
    public static Coin Create(Side firstSide, Side secondSide, float frontBackCoefficient = 1.0f)
    {
        if (firstSide == null || secondSide == null)
        {
            Debug.LogError("CoinFactory.Create requires two non-null Side assets.");
            return null;
        }

        Side frontSide;
        Side backSide;

        if (firstSide.FrontOrBack == FrontAndBack.Front && secondSide.FrontOrBack == FrontAndBack.Back)
        {
            frontSide = firstSide;
            backSide = secondSide;
        }
        else if (firstSide.FrontOrBack == FrontAndBack.Back && secondSide.FrontOrBack == FrontAndBack.Front)
        {
            frontSide = secondSide;
            backSide = firstSide;
        }
        else
        {
            Debug.LogWarning("CoinFactory.Create expected one Front and one Back. Using first as Front and second as Back.");
            frontSide = firstSide;
            backSide = secondSide;
        }

        return new Coin(frontSide, backSide, frontBackCoefficient);
    }
}