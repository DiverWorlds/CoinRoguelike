using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideInventoryCraftButton : MonoBehaviour
{
    [SerializeField] private PlayerSideInventory sideInventory;
    [SerializeField] private PlayerCoinInventory coinInventory;
    [SerializeField] private SideRankToggleBinding[] sideToggles;
    [SerializeField] private Button craftButton;
    [SerializeField] private float frontBackCoefficient = 1.0f;

    private void Awake()
    {
        if (craftButton == null)
        {
            craftButton = GetComponent<Button>();
        }

        if (craftButton != null)
        {
            craftButton.onClick.RemoveListener(OnClickCraftSelectedSides);
            craftButton.onClick.AddListener(OnClickCraftSelectedSides);
        }
        else
        {
            Debug.LogWarning("[SideInventoryCraftButton] Button component was not found. OnClick will not fire automatically.", this);
        }
    }

    private void OnEnable()
    {
        LogCurrentCoins();
    }

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        Refresh();
    }

    public void OnClickCraftSelectedSides()
    {
        if (sideInventory == null)
        {
            Debug.LogWarning("[SideInventoryCraftButton] PlayerSideInventory is not assigned.");
            return;
        }

        List<SideRankToggleBinding> selected = GetSelectedBindings();
        if (selected.Count != 2)
        {
            Debug.LogWarning($"[SideInventoryCraftButton] Craft requires exactly 2 selected side toggles, but selected={selected.Count}. {GetSelectionDebugText()}");
            return;
        }

        Side first = selected[0].Side;
        Side second = selected[1].Side;
        string firstName = first != null ? first.SideName : "(null)";
        string secondName = second != null ? second.SideName : "(null)";
        Debug.Log($"[SideInventoryCraftButton] Crafting with selected sides: {firstName} + {secondName}");
        sideInventory.CraftCoinFromOwnedSides(first, second, frontBackCoefficient);

        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].SetSelected(false);
        }

        Refresh();
    }

    private void Refresh()
    {
        if (sideToggles == null)
        {
            if (craftButton != null)
            {
                craftButton.interactable = false;
            }

            return;
        }

        for (int i = 0; i < sideToggles.Length; i++)
        {
            if (sideToggles[i] != null)
            {
                sideToggles[i].Refresh(sideInventory);
            }
        }

        if (craftButton != null)
        {
            craftButton.interactable = GetSelectedBindings().Count == 2;
        }
    }

    private List<SideRankToggleBinding> GetSelectedBindings()
    {
        List<SideRankToggleBinding> selected = new List<SideRankToggleBinding>();
        if (sideToggles == null)
        {
            return selected;
        }

        for (int i = 0; i < sideToggles.Length; i++)
        {
            SideRankToggleBinding binding = sideToggles[i];
            if (binding != null && binding.IsSelected)
            {
                selected.Add(binding);
            }
        }

        return selected;
    }

    private string GetSelectionDebugText()
    {
        if (sideToggles == null || sideToggles.Length == 0)
        {
            return "No side toggles assigned.";
        }

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append("Toggle states: ");

        for (int i = 0; i < sideToggles.Length; i++)
        {
            SideRankToggleBinding binding = sideToggles[i];
            if (binding == null)
            {
                builder.Append($"[{i}:null] ");
                continue;
            }

            string sideName = binding.Side != null ? binding.Side.SideName : "(null side)";
            builder.Append($"[{i}:{sideName}={binding.IsSelected}] ");
        }

        return builder.ToString();
    }

    private void LogCurrentCoins()
    {
        if (coinInventory == null)
        {
            Debug.LogWarning("[SideInventoryCraftButton] Coin inventory is not assigned, so current coin parameters could not be logged.", this);
            return;
        }

        IReadOnlyList<Coin> coins = coinInventory.GetCoins();
        if (coins.Count == 0)
        {
            Debug.Log("[SideInventoryCraftButton] Current coins: none.", this);
            return;
        }

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("[SideInventoryCraftButton] Current coin parameters:");

        for (int i = 0; i < coins.Count; i++)
        {
            Coin coin = coins[i];
            builder.AppendLine(
                $"  Slot {i + 1}: Front={coin.FrontSide.SideName}({coin.FrontSide.Rank}), Back={coin.BackSide.SideName}({coin.BackSide.Rank}), " +
                $"P(F)={coin.FrontProbability:F3}, P(B)={coin.BackProbability:F3}, " +
                $"FrontPerf={coin.FrontPerformanceValue:F2}, BackPerf={coin.BackPerformanceValue:F2}");
        }

        Debug.Log(builder.ToString(), this);
    }
}
