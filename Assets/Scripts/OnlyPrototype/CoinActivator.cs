using UnityEngine;

/// <summary>
/// Tosses the coin held by CoinCrafter, executes the matching SideAction,
/// and logs the toss result plus all coin fields.
/// Attach to a Button's OnClick.
/// </summary>
public class CoinActivator : MonoBehaviour
{
    [SerializeField] private CoinCrafter coinCrafter;
    [SerializeField] private Character actor;
    [SerializeField] private Character target;
    [SerializeField] private SideActionBase frontAction;
    [SerializeField] private SideActionBase backAction;

    public void Activate()
    {
        Coin coin = coinCrafter != null ? coinCrafter.CurrentCoin : null;

        if (coin == null)
        {
            Debug.LogWarning("[CoinActivator] No coin available. Run Craft first.");
            return;
        }

        FrontAndBack tossResult = coin.Toss();
        Side landedSide = tossResult == FrontAndBack.Front ? coin.FrontSide : coin.BackSide;
        SideActionBase action = tossResult == FrontAndBack.Front ? frontAction : backAction;

        Debug.Log(
            $"[Coin Toss]\n" +
            $"  Result       : {tossResult} ({landedSide.SideName})\n" +
            $"  FrontProb    : {coin.FrontProbability:F3}\n" +
            $"  BackProb     : {coin.BackProbability:F3}\n" +
            $"  PerformValue : {coin.FrontPerformanceValue:F2}\n" +
            CoinCrafter.GetCoinFieldsLog(coin)
        );

        if (action == null)
        {
            Debug.LogWarning($"[CoinActivator] No {tossResult} action assigned.");
            return;
        }

        float effectPower = coin.GetPerformanceValueFor(landedSide);
        action.Execute(actor, target, landedSide, effectPower);
    }
}
