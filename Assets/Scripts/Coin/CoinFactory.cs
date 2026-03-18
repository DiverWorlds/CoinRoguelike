using UnityEngine;

public class CoinFactory : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
    [SerializeField] private SideInventory sideInventory;

	public Coin CreateCoin(BaseSide frontSide, BaseSide backSide)
	{
		if (coinPrefab == null)
		{
			return null;
		}

		Coin coinInstance = Instantiate(coinPrefab);
        if (coinInstance == null)
        {
            return null;
        }

        sideInventory.Remove(frontSide);
        sideInventory.Remove(backSide);

		float frontSideProbability = CalcFrontSideProbability(frontSide, backSide);
		int frontSideValue = CalcFrontSideValue(frontSide);
		int backSideValue = CalcBackSideValue(backSide);

		coinInstance.SetSides(frontSide, backSide);
		coinInstance.Initialize(frontSideProbability, frontSideValue, backSideValue);
		return coinInstance;
	}

	private float CalcFrontSideProbability(BaseSide frontSide, BaseSide backSide)
	{
		float frontWeight = frontSide.Weight;
		float backWeight = backSide.Weight;
		float totalWeight = frontWeight + backWeight;

		if (totalWeight <= 0f)
		{
			return 0.5f;
		}

		float probability = backWeight / totalWeight;
		return Mathf.Round(probability * 1000f) / 1000f;
	}

	private int CalcFrontSideValue(BaseSide frontSide)
	{
		// TODO: ちゃんと作る
		return frontSide.Strength;
	}

	private int CalcBackSideValue(BaseSide backSide)
	{
		// TODO: ちゃんと作る
		return backSide.Strength;
	}
}
