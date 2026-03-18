using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSideInventory : MonoBehaviour
{
    private const float CurrentTierWeight = 3.0f;
    private const float LowerTierWeight = 0.8f;

    [SerializeField] private PlayerCoinInventory coinInventory;
    [SerializeField] private SideActionCatalog sideActionCatalog;
    [SerializeField] private float craftFrontBackCoefficient = 1.0f;
    [SerializeField] private bool consumeSidesOnCraft = true;
    [SerializeField] private bool grantStartingCoinsOnStart = true;

    private readonly List<Side> ownedSides = new List<Side>();
    private bool hasGrantedStartingCoins;

    private void Start()
    {
        if (grantStartingCoinsOnStart)
        {
            GrantStartingCoinsAtGameStart();
        }
    }

    public IReadOnlyList<Side> GetOwnedSides()
    {
        return ownedSides;
    }

    public int GetOwnedSideCount(Side side)
    {
        if (side == null)
        {
            return 0;
        }

        return CountSide(side);
    }

    public void AcquireAllSidesFromCatalog(int countEach = 2)
    {
        if (sideActionCatalog == null)
        {
            Debug.LogWarning("[PlayerSideInventory] SideActionCatalog is not assigned.");
            return;
        }

        if (countEach <= 0)
        {
            Debug.LogWarning("[PlayerSideInventory] countEach must be >= 1. Fallback to 2.");
            countEach = 2;
        }

        IReadOnlyList<Side> allSides = sideActionCatalog.GetAllSides();
        if (allSides.Count == 0)
        {
            Debug.LogWarning("[PlayerSideInventory] No sides found in SideActionCatalog.");
            return;
        }

        for (int i = 0; i < allSides.Count; i++)
        {
            for (int j = 0; j < countEach; j++)
            {
                AcquireSide(allSides[i]);
            }
        }

        Debug.Log($"[PlayerSideInventory] Acquired {allSides.Count} types x{countEach} each.");
    }

    public void AcquireSide(Side side)
    {
        if (side == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Acquire failed. Side is null.");
            return;
        }

        ownedSides.Add(side);
        Debug.Log($"[PlayerSideInventory] Acquired: {side.SideName} (Rank={side.Rank}, Value={side.Value}, Weight={side.Weight})");
    }

    public void LogOwnedSides()
    {
        if (ownedSides.Count == 0)
        {
            Debug.Log("[PlayerSideInventory] No sides owned.");
            return;
        }

        Dictionary<Side, int> counts = new Dictionary<Side, int>();
        for (int i = 0; i < ownedSides.Count; i++)
        {
            Side side = ownedSides[i];
            if (side == null)
            {
                continue;
            }

            if (!counts.ContainsKey(side))
            {
                counts[side] = 0;
            }

            counts[side]++;
        }

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("[PlayerSideInventory] Owned sides:");

        foreach (KeyValuePair<Side, int> pair in counts)
        {
            Side side = pair.Key;
            builder.AppendLine($"  - {side.SideName} x{pair.Value} ({side.FrontOrBack}, Rank={side.Rank}, Value={side.Value}, Weight={side.Weight})");
        }

        Debug.Log(builder.ToString());
    }

    public void CraftAttackAndMissCoinForDebug()
    {
        if (sideActionCatalog == null)
        {
            Debug.LogWarning("[PlayerSideInventory] SideActionCatalog is not assigned.");
            return;
        }

        CraftCoinByActionPairForDebug<SideAttackAction, SideMissAction>("Attack+Miss");
        CraftCoinByActionPairForDebug<SideHealAction, SideMissAction>("Heal+Miss");
    }

    public void GrantStartingCoinsAtGameStart()
    {
        if (hasGrantedStartingCoins)
        {
            return;
        }

        if (sideActionCatalog == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Cannot grant starting coins. SideActionCatalog is not assigned.");
            return;
        }

        hasGrantedStartingCoins = true;

        CraftCoinByActionPairForDebug<SideAttackAction, SideMissAction>("Start Attack+Miss");
        CraftCoinByActionPairForDebug<SideHealAction, SideMissAction>("Start Heal+Miss");
        CraftCoinByActionPairForDebug<SideAttackAction, SideMisfireAction>("Start Attack+Misfire");
    }

    public void AcquireRandomSideFromCatalog()
    {
        int stage = BattleManager.Instance != null ? BattleManager.Instance.CurrentStage : 1;
        AcquireRandomSideFromCatalog(stage);
    }

    public void AcquireRandomSideFromCatalog(int stage)
    {
        if (sideActionCatalog == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Cannot acquire random side. SideActionCatalog is not assigned.");
            return;
        }

        IReadOnlyList<Side> allSides = sideActionCatalog.GetAllSides();
        if (allSides.Count == 0)
        {
            Debug.LogWarning("[PlayerSideInventory] Cannot acquire random side. Catalog is empty.");
            return;
        }

        Rank unlockedRank = GetUnlockedRankForStage(stage);
        List<Side> candidates = new List<Side>();
        List<float> weights = new List<float>();

        for (int i = 0; i < allSides.Count; i++)
        {
            Side side = allSides[i];
            if (side == null)
            {
                continue;
            }

            if ((int)side.Rank < (int)unlockedRank)
            {
                continue;
            }

            float rarityWeight = side.Rank == unlockedRank ? CurrentTierWeight : LowerTierWeight;
            candidates.Add(side);
            weights.Add(rarityWeight);
        }

        if (candidates.Count == 0)
        {
            Debug.LogWarning($"[PlayerSideInventory] No candidate side for stage {stage} (unlocked rank: {unlockedRank}).");
            return;
        }

        int chosenIndex = GetWeightedRandomIndex(weights);
        Side chosenSide = candidates[chosenIndex];
        Debug.Log($"[PlayerSideInventory] Stage {stage} reward roll. Unlocked rank={unlockedRank}, chosen={chosenSide.SideName} ({chosenSide.Rank}).");
        AcquireSide(chosenSide);
    }

    private Rank GetUnlockedRankForStage(int stage)
    {
        if (stage >= 41)
        {
            return Rank.S;
        }

        if (stage >= 31)
        {
            return Rank.A;
        }

        if (stage >= 21)
        {
            return Rank.B;
        }

        if (stage >= 11)
        {
            return Rank.C;
        }

        return Rank.D;
    }

    private int GetWeightedRandomIndex(List<float> weights)
    {
        float total = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            total += Mathf.Max(0f, weights[i]);
        }

        if (total <= 0f)
        {
            return Random.Range(0, weights.Count);
        }

        float roll = Random.value * total;
        float accum = 0f;
        for (int i = 0; i < weights.Count; i++)
        {
            accum += Mathf.Max(0f, weights[i]);
            if (roll <= accum)
            {
                return i;
            }
        }

        return weights.Count - 1;
    }

    private void CraftCoinByActionPairForDebug<TFirstAction, TSecondAction>(string pairLabel)
        where TFirstAction : SideActionBase
        where TSecondAction : SideActionBase
    {
        Side firstSide = FindFirstOwnedSideByAction<TFirstAction>();
        Side secondSide = FindFirstOwnedSideByAction<TSecondAction>();

        if (firstSide == null)
        {
            Side firstFromCatalog = sideActionCatalog.GetFirstSideByAction<TFirstAction>();
            if (firstFromCatalog != null)
            {
                AcquireSide(firstFromCatalog);
                firstSide = firstFromCatalog;
            }
        }

        if (secondSide == null)
        {
            Side secondFromCatalog = sideActionCatalog.GetFirstSideByAction<TSecondAction>();
            if (secondFromCatalog != null)
            {
                AcquireSide(secondFromCatalog);
                secondSide = secondFromCatalog;
            }
        }

        if (firstSide == null || secondSide == null)
        {
            Debug.LogWarning($"[PlayerSideInventory] Debug craft failed for {pairLabel}. Required side mapping is missing in SideActionCatalog.");
            return;
        }

        CraftCoinFromOwnedSides(firstSide, secondSide, craftFrontBackCoefficient);
    }

    public void CraftCoinFromOwnedSides(Side firstSide, Side secondSide, float frontBackCoefficient)
    {
        if (firstSide == null || secondSide == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Craft failed. Two sides are required.");
            return;
        }

        if (!HasSide(firstSide) || !HasSide(secondSide) || (firstSide == secondSide && CountSide(firstSide) < 2))
        {
            Debug.LogWarning("[PlayerSideInventory] Craft failed. Required sides are not in inventory.");
            return;
        }

        CraftCoin(firstSide, secondSide, frontBackCoefficient);
    }

    public void CraftCoin(Side firstSide, Side secondSide, float frontBackCoefficient)
    {
        if (coinInventory == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Coin inventory is not assigned.");
            return;
        }

        if (firstSide == null || secondSide == null)
        {
            Debug.LogWarning("[PlayerSideInventory] Craft failed. Two sides are required.");
            return;
        }

        if (!HasSide(firstSide) || !HasSide(secondSide) || (firstSide == secondSide && CountSide(firstSide) < 2))
        {
            Debug.LogWarning("[PlayerSideInventory] Craft failed. Required sides are not in inventory.");
            return;
        }

        if (consumeSidesOnCraft)
        {
            ownedSides.Remove(firstSide);
            ownedSides.Remove(secondSide);
        }

        Coin coin = CoinFactory.Create(firstSide, secondSide, frontBackCoefficient);
        if (coin == null)
        {
            if (consumeSidesOnCraft)
            {
                ownedSides.Add(firstSide);
                ownedSides.Add(secondSide);
            }

            return;
        }

        if (!coinInventory.TryAddCoin(coin))
        {
            if (consumeSidesOnCraft)
            {
                ownedSides.Add(firstSide);
                ownedSides.Add(secondSide);
            }

            Debug.LogWarning("[PlayerSideInventory] Craft canceled. Coin inventory is full.");
            return;
        }

        Debug.Log("[PlayerSideInventory] Crafted coin successfully.\n" + CoinCrafter.GetCoinFieldsLog(coin));
    }

    private bool HasSide(Side side)
    {
        return ownedSides.Contains(side);
    }

    private int CountSide(Side side)
    {
        int count = 0;
        for (int i = 0; i < ownedSides.Count; i++)
        {
            if (ownedSides[i] == side)
            {
                count++;
            }
        }

        return count;
    }

    private Side FindFirstOwnedSideByAction<TAction>() where TAction : SideActionBase
    {
        for (int i = 0; i < ownedSides.Count; i++)
        {
            Side side = ownedSides[i];
            if (side == null)
            {
                continue;
            }

            SideActionBase action = sideActionCatalog.GetActionFor(side);
            if (action is TAction)
            {
                return side;
            }
        }

        return null;
    }
}
