using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private SideInventory sideInventory;
	[SerializeField] private CoinInventory coinInventory;
	[SerializeField] private Transform previewSlot;
	[SerializeField] private List<SideRecord> previewedRecords = new List<SideRecord>();
	[SerializeField] private float BonusMagnification = 3;

	public Coin CreatePreviewCoin(SideRecord side1, SideRecord side2)
	{
		if (side1.Side.FrontOrBack == side2.Side.FrontOrBack)
		{
			Debug.LogError("Cannot create coin with two sides of the same type.");
			return null;
		}

		SideRecord frontSideRecord = side1.Side.FrontOrBack == FrontAndBack.Front ? side1 : side2;
		SideRecord backSideRecord = side1.Side.FrontOrBack == FrontAndBack.Back ? side1 : side2;
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
		previewedRecords.Add(frontSideRecord);
		previewedRecords.Add(backSideRecord);

		return CreateCoinBase(frontSideRecord.Side, backSideRecord.Side, previewSlot, false);
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

		coinInventory.CoinSlotAssorter.AssignToEmptySlot(coin.transform);

		for (int i = previewedRecords.Count - 1; i >= 0; i--)
		{
			SideRecord record = previewedRecords[i];
			previewedRecords.RemoveAt(i);
			if (record != null)
			{
				record.ToggleImageVisualize(false);
			}
		}

		coinInventory.Add(coin);
	}
	public Coin CreateCoin(BaseSide side1, BaseSide side2)
	{
		Coin newCoin = CreateCoinBase(side1, side2, transform, true);
		coinInventory.CoinSlotAssorter.AssignToEmptySlot(newCoin.transform);
		return newCoin;
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
		BaseSide frontSide = side1.FrontOrBack == FrontAndBack.Front ? side1 : side2;
		BaseSide backSide = side1.FrontOrBack == FrontAndBack.Back ? side1 : side2;

		Coin coinInstance = Instantiate(coinPrefab, parent, false);

		if (addToInventory)
		{
			coinInventory.Add(coinInstance);
		}

		float frontSideProbability = CalcFrontSideProbability(frontSide, backSide);
		float backSideBonus = CalcBackSideBonus(frontSideProbability);
		int frontSideValue = CalcFrontSideValue(frontSide, backSideBonus);
		int backSideValue = CalcBackSideValue(backSide);

		coinInstance.Initialize(frontSide, backSide, frontSideProbability, frontSideValue, backSideValue, backSideBonus);

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
		Logger.Log("CalcBonusFrontSideName: " + frontSide.EffectName);
		if (frontSide.EffectName != "睡眠") return Mathf.RoundToInt(frontSide.Strength * backSideBonus);
		else return Mathf.RoundToInt(frontSide.Strength);
	}

	private int CalcBackSideValue(BaseSide backSide)
	{
		// TODO: ちゃんと作る
		return backSide.Strength;
	}
	private float CalcBackSideBonus(float frontSideProbability)
	{
		//1 + (1 - frontSideProbability) みたいな感じで、表の出る確率が低いほど裏のボーナスが高くなる
		return (2 - frontSideProbability) * BonusMagnification;
	}
}