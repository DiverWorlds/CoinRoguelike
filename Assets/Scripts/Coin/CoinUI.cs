using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class CoinUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private CoinDescriptionWindow coinDescriptionWindow;
	private BattleManager battleManager;
	private CoinInventory coinInventory;
    private Coin coin;

	private void Awake()
	{
		Debug.Log("CoinUI.Awake() called");
		coinDescriptionWindow = FindFirstObjectByType<CoinDescriptionWindow>();
		Debug.Log($"CoinDescriptionWindow found: {(coinDescriptionWindow != null ? "YES" : "NO")}");
		battleManager = FindFirstObjectByType<BattleManager>();
		Player player = FindFirstObjectByType<Player>();
		coinInventory = player.CoinInventory;
	}

	void Start()
    {
        coin = GetComponent<Coin>();
        Debug.Log($"CoinUI.Start() - coin component found: {(coin != null ? "YES" : "NO")}");
    }
    public void OnPointerEnter(PointerEventData eventData)
	{
        Debug.Log($"CoinUI.OnPointerEnter() called");
        Debug.Log($"coin: {(coin != null ? "NOT NULL" : "NULL")}");
        if (coin != null)
        {
            Debug.Log($"Coin data: FrontEffectName={coin.FrontEffectName}, BackEffectName={coin.BackEffectName}, FrontSideValue={coin.FrontSideValue}, BackSideValue={coin.BackSideValue}, FrontSideProbability={coin.FrontSideProbability}");
        }
        Debug.Log($"coinDescriptionWindow: {(coinDescriptionWindow != null ? "NOT NULL" : "NULL")}");
        if (coinDescriptionWindow != null && coin != null)
        {
            Debug.Log("Calling coinDescriptionWindow.Set()");
            coinDescriptionWindow.Set(coin.FrontEffectName, coin.BackEffectName, coin.FrontSideValue, coin.BackSideValue, coin.FrontSideProbability);
        }
        else
        {
            Debug.Log("SKIPPED: coinDescriptionWindow or coin is NULL");
        }
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		coinDescriptionWindow.Hide();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int coinIndex = coinInventory.IndexOf(coin);
		battleManager.ExecutePlayerCoinEffect(coinIndex);
	}


}