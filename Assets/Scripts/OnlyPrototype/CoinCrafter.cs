using UnityEngine;

/// <summary>
/// Synthesizes a Coin from two Side assets.
/// Attach to a Button's OnClick.
/// </summary>
public class CoinCrafter : MonoBehaviour
{
    [SerializeField] private Side firstSide;
    [SerializeField] private Side secondSide;
    [SerializeField] private float frontBackCoefficient = 1.0f;

    public Coin CurrentCoin { get; private set; }

    public void Craft()
    {
        CurrentCoin = CoinFactory.Create(firstSide, secondSide, frontBackCoefficient);

        if (CurrentCoin == null)
        {
            return;
        }

        Debug.Log(GetCoinFieldsLog(CurrentCoin));
    }

    public static string GetCoinFieldsLog(Coin coin)
    {
        return
            $"[Coin Crafted]\n" +
            $"  FrontSide    : {coin.FrontSide.SideName} (value={coin.FrontSide.Value}, weight={coin.FrontSide.Weight}, rank={coin.FrontSide.Rank})\n" +
            $"  BackSide     : {coin.BackSide.SideName} (value={coin.BackSide.Value}, weight={coin.BackSide.Weight}, rank={coin.BackSide.Rank})\n" +
            $"  FrontProb    : {coin.FrontProbability:F3}\n" +
            $"  BackProb     : {coin.BackProbability:F3}\n" +
            $"  Coefficient  : {coin.FrontBackCoefficient}\n" +
            $"  PerformValue : {coin.FrontPerformanceValue:F2}";
    }
}
