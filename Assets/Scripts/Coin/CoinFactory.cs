using UnityEngine;

public class CoinFactory : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private SideInventory sideInventory;
	[SerializeField] private CoinInventory coinInventory;
	[SerializeField] private Transform coin1Slot;
	[SerializeField] private Transform coin2Slot;
	[SerializeField] private Transform coin3Slot;
	[SerializeField] private Transform previewSlot;

	public Coin CreatePreviewCoin(BaseSide side1, BaseSide side2)
	{
		if (previewSlot != null && previewSlot.childCount > 0)
		{
			for (int i = previewSlot.childCount - 1; i >= 0; i--)
			{
				Coin existingCoin = previewSlot.GetChild(i).GetComponent<Coin>();
				if (existingCoin != null)
				{
					BaseSide[] sides = existingCoin.GetComponentsInChildren<BaseSide>();
					foreach (BaseSide side in sides)
					{
						side.transform.SetParent(sideInventory.transform, false);
					}

					Destroy(existingCoin.gameObject);
				}
			}
		}

		return CreateCoinBase(side1, side2, previewSlot, false);
	}
	public void MoveToSlot(Coin coin)
	{
		if (coin == null)
		{
			return;
		}

		if (coinInventory.IndexOf(coin) >= 0)
		{
			return;
		}

		int targetIndex = coinInventory.CoinCount;
		Transform parentSlot = GetSlotByIndex(targetIndex);
		if (parentSlot == null)
		{
			Debug.LogWarning($"Coin slot is not assigned for index {targetIndex}. Coin move was skipped.");
			return;
		}

		coin.transform.SetParent(parentSlot, false);
		coin.transform.localPosition = Vector3.zero;
		coin.transform.localRotation = Quaternion.identity;

		coinInventory.Add(coin);
	}
	public Coin CreateCoin(BaseSide side1, BaseSide side2)
	{
		int targetIndex = coinInventory.CoinCount;
		Transform parentSlot = GetSlotByIndex(targetIndex);
		if (parentSlot == null)
		{
			Debug.LogWarning($"Coin slot is not assigned for index {targetIndex}. Coin creation was skipped.");
			return null;
		}

		return CreateCoinBase(side1, side2, parentSlot, true);
	}
	private Coin CreateCoinBase(BaseSide side1, BaseSide side2, Transform parent, bool addToInventory)
	{
		if (side1.FrontOrBack == side2.FrontOrBack)
		{
			Debug.LogError("Cannot create coin with two sides of the same type.");
			return null;
		}
		if (parent == null)
		{
			Debug.LogWarning("Parent is not assigned. Coin creation was skipped.");
			return null;
		}

		Coin coinInstance = Instantiate(coinPrefab, parent, false);

		if (addToInventory)
		{
			coinInventory.Add(coinInstance);
		}

		float frontSideProbability = CalcFrontSideProbability(side1, side2);
		float backSideBonus = CalcBackSideBonus(frontSideProbability);
		int frontSideValue = CalcFrontSideValue(side1, backSideBonus);
		int backSideValue = CalcBackSideValue(side2);

		coinInstance.Initialize(side1, side2, frontSideProbability, frontSideValue, backSideValue, backSideBonus);

		side1.transform.SetParent(coinInstance.transform, false);
		side2.transform.SetParent(coinInstance.transform, false);

		coinInstance.transform.localPosition = Vector3.zero;
		coinInstance.transform.localRotation = Quaternion.identity;

		return coinInstance;
	}
	public Coin CombineSides(BaseSide side1, BaseSide side2)
	{
		sideInventory.Remove(side1);
		sideInventory.Remove(side2);
		return CreateCoin(side1, side2);
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

	private int CalcFrontSideValue(BaseSide frontSide, float backSideBonus)
	{
		//丸め込み
		return Mathf.RoundToInt(frontSide.Strength * backSideBonus);
	}

	private int CalcBackSideValue(BaseSide backSide)
	{
		// TODO: ちゃんと作る
		return backSide.Strength;
	}
	private float CalcBackSideBonus(float frontSideProbability)
	{
		//1 + (1 - frontSideProbability) みたいな感じで、表の出る確率が低いほど裏のボーナスが高くなる
		return 2 - frontSideProbability;
	}
}
