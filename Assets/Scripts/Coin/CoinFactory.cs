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
		if (coinPrefab == null || coinInventory == null)
		{
			return null;
		}

		int targetIndex = coinInventory.CoinCount;
		Transform parentSlot = GetSlotByIndex(targetIndex);
		if (parentSlot == null)
		{
			Debug.LogWarning($"Coin slot is not assigned for index {targetIndex}. Coin creation was skipped.");
			return null;
		}

		Coin coinInstance = Instantiate(coinPrefab, parentSlot, false);
        if (coinInstance == null)
        {
            return null;
        }

		if (!coinInventory.Add(coinInstance))
		{
			Destroy(coinInstance.gameObject);
			return null;
		}

        sideInventory.Remove(frontSide);
        sideInventory.Remove(backSide);

		float frontSideProbability = CalcFrontSideProbability(frontSide, backSide);
		int frontSideValue = CalcFrontSideValue(frontSide);
		int backSideValue = CalcBackSideValue(backSide);

		coinInstance.SetSides(frontSide, backSide);
		coinInstance.Initialize(frontSideProbability, frontSideValue, backSideValue);

		coinInstance.transform.localPosition = Vector3.zero;
		coinInstance.transform.localRotation = Quaternion.identity;

		return coinInstance;
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
