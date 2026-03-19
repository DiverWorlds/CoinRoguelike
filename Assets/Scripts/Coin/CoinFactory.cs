using UnityEngine;

public class CoinFactory : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
    [SerializeField] private SideInventory sideInventory;
	[SerializeField] private CoinInventory coinInventory;
    [SerializeField] private Transform coin1Slot;
    [SerializeField] private Transform coin2Slot;
    [SerializeField] private Transform coin3Slot;

    public Coin CreateCoin(BaseSide frontSide, BaseSide backSide)
    {
		int targetIndex = coinInventory.CoinCount;
		Transform parentSlot = GetSlotByIndex(targetIndex);
		if (parentSlot == null)
		{
			Debug.LogWarning($"Coin slot is not assigned for index {targetIndex}. Coin creation was skipped.");
			return null;
		}

		Coin coinInstance = Instantiate(coinPrefab, parentSlot, false);
        
        coinInventory.Add(coinInstance);

		float frontSideProbability = CalcFrontSideProbability(frontSide, backSide);
		int frontSideValue = CalcFrontSideValue(frontSide);
		int backSideValue = CalcBackSideValue(backSide);
        
		coinInstance.Initialize(frontSide, backSide, frontSideProbability, frontSideValue, backSideValue);

		frontSide.transform.SetParent(coinInstance.transform, false);
		backSide.transform.SetParent(coinInstance.transform, false);

		coinInstance.transform.localPosition = Vector3.zero;
		coinInstance.transform.localRotation = Quaternion.identity;

		return coinInstance;
    }
	public Coin CombineSides(BaseSide frontSide, BaseSide backSide)
	{
        sideInventory.Remove(frontSide);
        sideInventory.Remove(backSide);
        return CreateCoin(frontSide, backSide);
	}

	private Transform GetSlotByIndex(int index)
	{
		switch (index)
		{
			case 0:
				return coin1Slot;
			case 1:
				return coin2Slot;
			case 2:
				return coin3Slot;
			default:
				return null;
		}
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
