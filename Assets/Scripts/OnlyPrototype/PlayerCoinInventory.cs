using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Player))]
public class PlayerCoinInventory : MonoBehaviour
{
    private const int MaxCoinCount = 3;

    [SerializeField] private Character actor;
    [SerializeField] private SideActionCatalog sideActionCatalog;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private int selectedSlot = 1;

    private readonly List<Coin> ownedCoins = new List<Coin>();

    public event Action CoinsChanged;

    public IReadOnlyList<Coin> GetCoins()
    {
        return ownedCoins;
    }

    public bool TryAddCoin(Coin coin)
    {
        if (coin == null)
        {
            return false;
        }

        if (ownedCoins.Count >= MaxCoinCount)
        {
            Debug.LogWarning("[PlayerCoinInventory] Cannot add coin. Inventory is full (max 3).");
            return false;
        }

        ownedCoins.Add(coin);
        Debug.Log("[PlayerCoinInventory] Coin added.\n" + CoinCrafter.GetCoinFieldsLog(coin));
        CoinsChanged?.Invoke();
        return true;
    }

    public void LogCoins()
    {
        if (ownedCoins.Count == 0)
        {
            Debug.Log("[PlayerCoinInventory] No coins owned.");
            return;
        }

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("[PlayerCoinInventory] Coins (slot 1-3):");

        for (int i = 0; i < MaxCoinCount; i++)
        {
            if (i >= ownedCoins.Count)
            {
                builder.AppendLine($"  {i + 1}: (empty)");
                continue;
            }

            Coin coin = ownedCoins[i];
            builder.AppendLine(
                $"  {i + 1}: Front={coin.FrontSide.SideName}, Back={coin.BackSide.SideName}, " +
                $"P(F)={coin.FrontProbability:F3}, P(B)={coin.BackProbability:F3}, Perf={coin.FrontPerformanceValue:F2}");
        }

        Debug.Log(builder.ToString());
    }

    public void DiscardSelectedCoin()
    {
        DiscardCoinAtSlot(selectedSlot);
    }

    public void DiscardCoin1()
    {
        DiscardCoinAtSlot(1);
    }

    public void DiscardCoin2()
    {
        DiscardCoinAtSlot(2);
    }

    public void DiscardCoin3()
    {
        DiscardCoinAtSlot(3);
    }

    public void DiscardCoinAtSlot(int slot)
    {
        int index = slot - 1;
        if (!IsValidCoinIndex(index))
        {
            Debug.LogWarning($"[PlayerCoinInventory] Discard failed. Slot {slot} is empty or invalid.");
            return;
        }

        Coin removed = ownedCoins[index];
        ownedCoins.RemoveAt(index);
        Debug.Log($"[PlayerCoinInventory] Discarded slot {slot}: {removed.FrontSide.SideName}/{removed.BackSide.SideName}");
        CoinsChanged?.Invoke();
    }

    public void UseSelectedCoin()
    {
        UseCoinAtSlot(selectedSlot);
    }

    public void UseCoin1()
    {
        UseCoinAtSlot(1);
    }

    public void UseCoin2()
    {
        UseCoinAtSlot(2);
    }

    public void UseCoin3()
    {
        UseCoinAtSlot(3);
    }

    public void UseCoinAtSlot(int slot)
    {
        int index = slot - 1;
        if (!IsValidCoinIndex(index))
        {
            Debug.LogWarning($"[PlayerCoinInventory] Use failed. Slot {slot} is empty or invalid.");
            return;
        }

        if (actor == null)
        {
            Debug.LogWarning("[PlayerCoinInventory] Use failed. Actor is not assigned.");
            return;
        }

        Coin coin = ownedCoins[index];
        FrontAndBack tossResult = coin.Toss();
        Side landedSide = tossResult == FrontAndBack.Front ? coin.FrontSide : coin.BackSide;

        Debug.Log(
            $"[PlayerCoinInventory] Use slot {slot}\n" +
            $"  TossResult   : {tossResult} ({landedSide.SideName})\n" +
            CoinCrafter.GetCoinFieldsLog(coin)
        );

        Character target = battleManager != null ? battleManager.EnemyCharacter : null;

        SideActionBase action = sideActionCatalog != null ? sideActionCatalog.GetActionFor(landedSide) : null;
        if (action == null)
        {
            Debug.LogWarning($"[PlayerCoinInventory] No action found for side: {landedSide.SideName}");
        }
        else
        {
            float effectPower = coin.GetPerformanceValueFor(landedSide);
            action.Execute(actor, target, landedSide, effectPower);
        }

        if (battleManager != null)
        {
            battleManager.CompletePlayerActionAndAdvanceToEnemyTurn();
        }

        CoinsChanged?.Invoke();
    }

    private bool IsValidCoinIndex(int index)
    {
        return index >= 0 && index < ownedCoins.Count;
    }
}
